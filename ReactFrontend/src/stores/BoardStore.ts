import { IObservableArray, makeAutoObservable, observable } from "mobx";
import { Equal, IsAdjacent } from "../utils/gridHelper";
import { Position } from "../utils/apiModels";
import { Player } from "../utils/types";
import { getGameState, move } from "../utils/api";

export class BoardStore {
  constructor() {
    makeAutoObservable(this);
  }

  selectedPlayer: Player | null = null;
  selectedPath: IObservableArray<Position> = observable.array();
  players: IObservableArray<Player> = observable.array();

  getGameState = async () => {
    const gameState = await getGameState();
    this.players = observable.array(gameState.players);
  }

  selectTile = (position: Position) => {
    const occupant = this.getTileOccupant(position);
    if (occupant) {
      this.selectedPlayer = occupant;
      this.selectedPath = observable.array();
      return;
    }

    if (this.selectedPlayer && this.selectedPath.length == 0 && this.selectedPlayer.currentAnimus > this.selectedPath.length && IsAdjacent(this.selectedPlayer, position)) {
      this.selectedPath.push(position);
      return;
    }

    if (this.selectedPlayer && this.selectedPath.length > 0 && this.selectedPlayer.currentAnimus > this.selectedPath.length && IsAdjacent(this.selectedPath[this.selectedPath.length - 1], position)) {
      this.selectedPath.push(position);
      return;
    }

    this.selectedPlayer = null;
    this.selectedPath = observable.array();
  }

  move = async () => {
    if (this.selectedPlayer && this.selectedPath.length > 0) {
      const gameState = await move({ playerId: this.selectedPlayer.id, positions: this.selectedPath });
      this.selectedPlayer = null;
      this.selectedPath = observable.array();
      this.players = observable.array(gameState.players); //todo: remove duplication
    }
  }

  tileIsHighlighted = (position: Position | null): boolean => {
    return this.selectedPath.filter(x => Equal(x, position)).length == 1;
  }

  getTileColor = (position: Position): string => {
    return Equal(this.selectedPlayer, position) ? "bg-stone-500" : this.tileIsHighlighted(position) ? "bg-stone-300" : "bg-stone-200";
  }

  getTileOccupant = (position: Position): Player | undefined => {
    return this.players.filter(player => Equal(player, position)).find(x => x);
  }
}