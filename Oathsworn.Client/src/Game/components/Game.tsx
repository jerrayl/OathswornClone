import { Observer } from "mobx-react";
import { Board } from "./Board";
import { GameStore } from "../stores/GameStore";

export const Game = () => {
    const gameStore = new GameStore();

    return (
        <Observer>
            {() => gameStore.gameState ? <Board boardStore={gameStore.boardStore} /> : <div className="w-full h-screen flex items-center justify-center"><h2>Loading...</h2></div>}
        </Observer>
    )
}