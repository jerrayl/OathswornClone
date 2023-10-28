import { IObservableArray, makeAutoObservable, observable } from "mobx";
import { completeAttack, rerollAttack, startAttack } from "../utils/api";
import { Might } from "../utils/apiModels";
import { MightCard, Player } from "../utils/types";

export class AttackStore {
  constructor(player: Player) {
    makeAutoObservable(this);
    this.player = player;
  }

  player: Player;
  enemyId = 1;
  attackId: number | null = null;
  isBossTargeted = true;
  empowerTokensUsed = 0;
  mightCards = observable.map<Might, number>({ [Might.White]: 0, [Might.Yellow]: 0, [Might.Red]: 0, [Might.Black]: 0 });
  cardsDrawn: IObservableArray<MightCard> | null = null;
  cardsToRedraw: IObservableArray<number> = observable.array();

  get redrawTokensUsed(): number {
    return this.cardsToRedraw.length;
  }

  mightChanged(might: Might, increased: boolean) {
    const newValue = this.mightCards.get(might)! + (increased ? 1 : -1);
    if (newValue >= 0) {
      this.mightCards.set(might, newValue);
    }
  }

  async toggleCardToRedraw(cardId: number) {
    if (this.cardsToRedraw.includes(cardId)){
      this.cardsToRedraw.remove(cardId);
    } else {
      this.cardsToRedraw.push(cardId);
    }
  }

  async drawCards() {
    var result = await startAttack({ playerId: this.player.id, enemyId: this.enemyId, might: Object.fromEntries(this.mightCards.toJSON()), empowerTokensUsed: this.empowerTokensUsed, isBossTargeted: this.isBossTargeted });
    this.attackId = result.attackId;
    this.cardsDrawn = observable.array(result.cardsDrawn);
  }

  async redrawCards() {
    var result = await rerollAttack({ attackId: this.attackId!, mightCards: this.cardsToRedraw.toJSON(), rerollTokensUsed: this.redrawTokensUsed });
    this.cardsDrawn = observable.array(result.cardsDrawn);
    console.log(result);
  }

  async completeAttack() {
    var result = await completeAttack(this.attackId!);
    console.log(result);
  }
}