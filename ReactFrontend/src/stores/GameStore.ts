import { makeAutoObservable } from "mobx";
import { GameStateModel } from "../utils/apiModels";
import { BoardStore } from "./BoardStore";
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { SIGNALR_CODES } from "../utils/constants";

export class GameStore {
  connection: HubConnection;
  gameState?: GameStateModel;
  boardStore: BoardStore = new BoardStore(() => this.gameState!);

  constructor() {
    makeAutoObservable(this);
    const newConnection = new HubConnectionBuilder()
      .withUrl(`https://localhost:5001/signalr`)
      .withAutomaticReconnect()
      .build();

    newConnection.on('GameState', message => {
      this.gameState = message;
      console.log(message);
    });
    this.connection = newConnection;
    this.startConnection();
  }

  startConnection = async () => {
    await this.connection.start();
    this.register();
  }

  register = async () => {
    // temporary hardcode
    const encounterId = 1;
    try {
      const response = await this.connection.invoke("register", encounterId);
      if (response === SIGNALR_CODES.SUCCESS) {
        console.log("successfully connected to SignalR")
      }
    }
    catch (e) {
      console.log(e);
    }
  }
}