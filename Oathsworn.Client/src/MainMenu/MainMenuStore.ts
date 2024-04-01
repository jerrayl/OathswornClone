import { makeAutoObservable } from "mobx";
import { Class, EncounterModel, FreeCompanyModel, PlayerSummaryModel } from "../utils/apiModels";
import { createFreeCompany, createPlayer, getEncounters, getFreeCompanies, getPlayers, joinFreeCompany } from "../utils/api";

export class NewPlayerForm {
  class: Class | null = null;
  name: string = '';

  constructor() {
    makeAutoObservable(this);
  }
}

export class FreeCompanyForm {
  name: string = '';

  constructor() {
    makeAutoObservable(this);
  }
}

export class MainMenuStore {
  encounters: EncounterModel[] = [];
  freeCompanies: FreeCompanyModel[] = [];
  players: PlayerSummaryModel[] = [];
  newPlayerForm: NewPlayerForm | null = null;
  freeCompanyForm: FreeCompanyForm | null = null;

  constructor() {
    makeAutoObservable(this);
    this.loadData();
  }

  loadData = async () => {
    this.players = await getPlayers();
    this.freeCompanies = await getFreeCompanies();
    this.encounters = await getEncounters();
  }

  createPlayer = async () => {
    if (!this.newPlayerForm || !this.newPlayerForm.class){
      return;
    }
    await createPlayer({ name: this.newPlayerForm.name, class: this.newPlayerForm.class })
    this.newPlayerForm = null;
    await this.loadData();
  }

  createFreeCompany = async (playerId: number) => {
    await createFreeCompany({ name: "test FC", playerId: playerId })
    await this.loadData();
  }

  joinFreeCompany = async (playerId: number) => {
    await joinFreeCompany({ code: "ABCD", playerId: playerId })
    await this.loadData();
  }

  showNewPlayerModal = () => {
    this.newPlayerForm = new NewPlayerForm();
  }

  startEncounter = () => {
  }
}