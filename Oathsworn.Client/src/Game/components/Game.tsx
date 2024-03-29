import { Observer } from "mobx-react";
import { Board } from "./Board";
import { GameStore } from "../stores/GameStore";

export const Game = () => {
    const gameStore = new GameStore();

    return (
        <Observer>
            {() => gameStore.gameState ? <Board boardStore={gameStore.boardStore} /> : <h2>Loading</h2>}
        </Observer>
    )
}