import { Observer } from "mobx-react";
import "./App.css";
import { Game } from "./components/Game";
import { GameStore } from "./stores/GameStore"

function App() {
    const gameStore = new GameStore();

    return (
        <Observer>
            {() => gameStore.gameState ? <Game boardStore={gameStore.boardStore} /> : <h2>Loading</h2>}
        </Observer>
    )
}

export default App;
