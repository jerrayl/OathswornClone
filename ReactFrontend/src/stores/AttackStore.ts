import { makeAutoObservable, observable } from "mobx";
import { Might } from "../utils/constants";

export class AttackStore {
  constructor() {
    makeAutoObservable(this);
  }

  mightCards = observable.map<Might, number>({[Might.White]: 0, [Might.Yellow]: 0, [Might.Red]: 0, [Might.Black]: 0 });

  mightChanged(might: Might, increased: boolean) {
    const newValue = this.mightCards.get(might)! + (increased ? 1 : -1);
    if (newValue >= 0){
        this.mightCards.set(might, newValue);
    }
  }
}