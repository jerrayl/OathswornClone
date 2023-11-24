import { IObservableArray, makeAutoObservable, observable } from "mobx";
import { Equal, IsAdjacent } from "../utils/gridHelper";
import { CharacterType, GameStateModel, PlayerModel, Position } from "../utils/apiModels";
import { continueEnemyAction, endTurn, move } from "../utils/api";
import { AttackStore } from "./AttackStore";
import { TileOccupant } from "../utils/types";
import { classIconMap } from "../assets/icons/ClassIcons";
import { borderIconMap } from "../assets/icons/BorderIcons";
import { bossIconMap } from "../assets/icons/BossIcons";

export class BoardStore {
  getGameState: () => GameStateModel;
  selectedPosition: Position | null = null;
  selectedPath: IObservableArray<Position> = observable.array();
  attackStore: AttackStore | null = null;
  pendingMove: boolean = false;

  constructor(getGameState: () => GameStateModel) {
    makeAutoObservable(this);
    this.getGameState = getGameState;
  }

  get selectedBossPart() {
    return this.selectedCharacter &&
      this.selectedCharacter.type == CharacterType.Boss &&
      this.getGameState().boss.positions.filter(p => p.xPosition == this.selectedPosition?.xPosition && p.yPosition == this.selectedPosition?.yPosition)[0];
  }

  get selectedPlayer() {
    return this.selectedCharacter &&
      this.selectedCharacter.type == CharacterType.Player &&
      this.getGameState().players.filter(x => x.id == this.selectedCharacter!.id)[0];
  }

  get selectedCharacter() {
    return this.selectedPosition && this.getTileOccupant(this.selectedPosition);
  }

  selectTile = (position: Position) => {
    const occupant = this.getTileOccupant(position);
    if (occupant) {
      this.selectedPosition = position;
      this.selectedPath = observable.array();
      return;
    }

    if (this.selectedPlayer && this.selectedPlayer.currentAnimus > this.selectedPath.length &&
      (this.selectedPath.length === 0 && IsAdjacent(this.selectedPosition!, position) ||
        this.selectedPath.length > 0 && IsAdjacent(this.selectedPath[this.selectedPath.length - 1], position))
    ) {
      this.selectedPath.push(position);
      return;
    }

    if (this.selectedPath.length > 0 && Equal(this.selectedPath[this.selectedPath.length - 1], position)) {
      this.pendingMove = true;
      return;
    }

    this.selectedPosition = null;
    this.selectedPath = observable.array();
  }

  cancelMove = () => {
    this.pendingMove = false;
    this.selectedPosition = null;
    this.selectedPath = observable.array();
  }

  attack = () => {
    if (this.selectedPlayer) {
      this.attackStore = new AttackStore(this.selectedPlayer);
    }
  }

  move = async () => {
    if (this.selectedPlayer && this.selectedPath.length > 0) {
      await move({ playerId: this.selectedPlayer.id, positions: this.selectedPath });
      this.selectedPosition = null;
      this.selectedPath = observable.array();
      this.pendingMove = false;
    }
  }

  endTurn = async () => {
    await endTurn();
  }

  continueEnemyAction = async () => {
    await continueEnemyAction();
  }

  tileIsHighlighted = (position: Position | null): boolean => {
    return this.selectedPath.filter(x => Equal(x, position)).length === 1;
  }

  getTileColor = (position: Position): string => {
    return Equal(this.selectedPosition, position) ? "bg-gray-300" : this.tileIsHighlighted(position) ? "bg-stone-300" : "bg-stone-200";
  }

  getTileOccupant = (position: Position): TileOccupant | undefined => {
    const gameState = this.getGameState();
    const player = gameState.players.filter(player => Equal(player, position)).find(x => x);
    const bossPosition = gameState.boss.positions.filter(bossPosition => Equal(bossPosition, position)).find(x => x);
    const bossBorder = bossPosition?.corner ?? bossPosition?.direction ?? "";
    return player ? { id: player.id, description: player.class.toString(), content: classIconMap[player.class], type: CharacterType.Player } :
      bossPosition ? { id: gameState.boss.id, description: "Boss " + bossBorder.toString(), content: bossBorder ? borderIconMap[bossBorder] : bossIconMap[gameState.boss.number], type: CharacterType.Boss } :
        undefined;
  }
}