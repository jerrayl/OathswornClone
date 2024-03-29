import { makeAutoObservable } from "mobx";
import { Class, EncounterModel, FreeCompanyModel, PlayerSummaryModel } from "../utils/apiModels";
import { createFreeCompany, createPlayer, getEncounters, getFreeCompanies, getPlayers, joinFreeCompany } from "../utils/api";

export class MainMenuStore {
  encounters: EncounterModel[] = [];
  freeCompanies: FreeCompanyModel[] = [];
  players: PlayerSummaryModel[] = [];

  constructor() {
    makeAutoObservable(this);
    this.loadData();
  }

  loadData = async () => {
    this.players = await getPlayers();
    this.freeCompanies = await getFreeCompanies();
    this.encounters =  await getEncounters();
  }

  createPlayer = async () => {
    await createPlayer({name: "test", class : Class.Witch})
    await this.loadData();
  }

  createFreeCompany = async (playerId: number) => {
    await createFreeCompany({name: "test FC", playerId : playerId})
    await this.loadData();
  }

  joinFreeCompany = async (playerId: number) => {
    await joinFreeCompany({code: "ABCD", playerId : playerId})
    await this.loadData();
  }
}