import { IObservableArray, makeAutoObservable, observable } from "mobx";
import { Equal, IsAdjacent } from "../utils/gridHelper";
import { Position } from "../utils/apiModels";
import { Player } from "../utils/types";
import { getGameState, move } from "../utils/api";
import { AttackStore } from "./AttackStore";

export class BoardStore {
  constructor() {
    makeAutoObservable(this);
  }

  selectedPlayerId: number | null = null;
  selectedPath: IObservableArray<Position> = observable.array();
  players: IObservableArray<Player> = observable.array();
  attackStore: AttackStore | null = null;
  pendingMove: boolean = false;

  get selectedPlayer () {
    return this.players.filter(x => x.id === this.selectedPlayerId)[0];
  }

  getGameState = async () => {
    const gameState = await getGameState();
    this.players = observable.array(gameState.players);
  }

  selectTile = (position: Position) => {
    const occupant = this.getTileOccupant(position);
    if (occupant) {
      this.selectedPlayerId = occupant.id;
      this.selectedPath = observable.array();
      return;
    }

    if (this.selectedPlayer && this.selectedPath.length === 0 && this.selectedPlayer.currentAnimus > this.selectedPath.length && IsAdjacent(this.selectedPlayer, position)) {
      this.selectedPath.push(position);
      return;
    }

    if (this.selectedPlayer && this.selectedPath.length > 0 && this.selectedPlayer.currentAnimus > this.selectedPath.length && IsAdjacent(this.selectedPath[this.selectedPath.length - 1], position)) {
      this.selectedPath.push(position);
      return;
    }

    if (this.selectedPath.length > 0 && Equal(this.selectedPath[this.selectedPath.length -1], position)){
      this.pendingMove = true;
      return;
    }

    this.selectedPlayerId = null;
    this.selectedPath = observable.array();
  }

  cancelMove = () => {
    this.pendingMove = false;
    this.selectedPlayerId = null;
    this.selectedPath = observable.array();
  }

  attack = () => {
    if (this.selectedPlayerId){
      this.attackStore = new AttackStore(this.selectedPlayer);
    }
  }

  move = async () => {
    if (this.selectedPlayer && this.selectedPath.length > 0) {
      const gameState = await move({ playerId: this.selectedPlayer.id, positions: this.selectedPath });
      this.selectedPlayerId = null;
      this.selectedPath = observable.array();
      this.players = observable.array(gameState.players); //todo: remove duplication
      this.pendingMove = false;
    }
  }

  tileIsHighlighted = (position: Position | null): boolean => {
    return this.selectedPath.filter(x => Equal(x, position)).length === 1;
  }

  getTileColor = (position: Position): string => {
    return Equal(this.selectedPlayer, position) ? "bg-gray-300" : this.tileIsHighlighted(position) ? "bg-stone-300" : "bg-stone-200";
  }

  getTileOccupant = (position: Position): Player | undefined => {
    return this.players.filter(player => Equal(player, position)).find(x => x);
  }
}