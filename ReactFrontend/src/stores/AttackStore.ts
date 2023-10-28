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
  mightCards = observable.map<Might, number>({ [Might.White]: 0, [Might.Yellow]: 0, [Might.Red]: 0, [Might.Black]: 0 });
  cardsDrawn: IObservableArray<MightCard> | null = null;
  cardsToRedraw: IObservableArray<number> = observable.array();

  getMightPoints(might: { [key in Might]: number }): number[] {
    return Array(might[Might.Black]).fill(3)
      .concat(Array(might[Might.Red]).fill(2))
      .concat(Array(might[Might.Yellow]).fill(1))
  }

  get mightPointsRequired(): number {
    const playerMightPoints = this.getMightPoints(this.player.might);
    const mightPointsRequired = this.getMightPoints(Object.fromEntries(this.mightCards.toJSON()))
      .map((value, i) => i < 3 && playerMightPoints.length > i && value >= playerMightPoints[i] ? value - playerMightPoints[i] : value);
    return mightPointsRequired.reduce((a, b) => a + b, 0);
  }

  get empowerTokensUsed(): number {
    return Math.ceil(this.mightPointsRequired / 3);
  }

  get leftoverEmpowerPoints(): boolean {
    return this.mightPointsRequired % 3 !== 0;
  }

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
    if (this.cardsToRedraw.includes(cardId)) {
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