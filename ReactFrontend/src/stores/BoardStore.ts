import { makeAutoObservable } from "mobx";
import { Position } from "../utils/types";
import { Equal, IsAdjacent, IsOnSameAxis } from "../utils/gridHelper";

export class BoardStore {
  constructor() {
    makeAutoObservable(this);
  }

  selectedTile: Position | null = null;

  tileIsHighlighted = (position: Position | null) : boolean => { 
    return !!position && !!this.selectedTile && IsOnSameAxis(this.selectedTile, position);
  }

  getTileColor = (position: Position) : string => {
    return Equal(this.selectedTile, position) ? "bg-red-500" : this.tileIsHighlighted(position) ? "bg-yellow-300" : "bg-stone-200";
  } 
}