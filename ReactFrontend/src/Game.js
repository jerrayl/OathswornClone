import title from "./assets/title.png";
import icons, { BONUS_ICONS } from "./assets/icons/icon";
import { useEffect, useState } from "react";
import BuildingMenu from "./components/BuildingMenu";
import TroopsMenu from "./components/TroopsMenu";
import { getBuildings } from "./utils/api";
import ELEMENT_ICONS from "./assets/elements/elements";
import buildingImages from "./assets/buildings/buildingImages";

function Game({ gameState, email, placeBuilding }) {

    const fetchBuildings = async () => {
        const response = await getBuildings();
        setBuildings(response);
    }

    useEffect(() => {
        fetchBuildings();
    }, [])

    useEffect(() => {
        if (gameState) {
            setPlayerState(gameState.players.filter(player => player.email == email)[0]);
            setOpponentState(gameState.players.filter(player => player.email != email)[0]);
        }
    }, [gameState])

    const getState = () => displayOwnState ? playerState : opponentState;

    const placeOnBoard = (boardSpace) => {
        if (selectedBuilding != null) {
            placeBuilding(boardSpace, selectedBuilding);
        }
    }

    const [hoverSpace, setHoverSpace] = useState(null);
    const [buildings, setBuildings] = useState([]);
    const [showBuildingMenu, setShowBuildingMenu] = useState(false);
    const [showTroopsMenu, setShowTroopsMenu] = useState(false);
    const [selectedElement, setSelectedElement] = useState("");
    const [displayOwnState, setDisplayOwnState] = useState(true);
    const [playerState, setPlayerState] = useState(null);
    const [opponentState, setOpponentState] = useState(null);
    const [selectedBuilding, setSelectedBuilding] = useState(null);

    return (
        <div className="flex flex-col items-center font-serif">
            <nav className="bg-stone-700 w-screen h-20 px-12 grid grid-cols-3 flex items-center text-3xl shadow-lg z-10">
                <div className="flex justify-start">
                    <h2 className="text-white">{gameState?.status ? gameState.status : "Game Starting..."}: {gameState?.currentPlayer ? gameState.currentPlayer == email ? "Your turn" : "Opponent's turn" : ""}</h2>
                </div>
                <div className="flex justify-center">
                    <img src={title} className="w-56" alt="title" />
                </div>
                <div className="flex justify-end">
                    <h2 className="text-white">End turn</h2>
                </div>
            </nav>
            {!showBuildingMenu && !showTroopsMenu && (
                <button
                    onClick={() => setShowBuildingMenu(true)}
                    className="absolute left-0 mt-36 bg-stone-700 hover:bg-stone-600 text-white text-4xl font-bold py-8 px-5 rounded-tr-lg rounded-br-lg shadow-lg"
                >
                    <img src={icons.building} />
                </button>
            )}
            {!showTroopsMenu && !showBuildingMenu && (
                <button
                    onClick={() => setShowTroopsMenu(true)}
                    className="absolute right-0 mt-36 bg-stone-700 hover:bg-stone-600 text-white text-4xl font-bold py-8 px-5 rounded-tl-lg rounded-bl-lg shadow-lg"
                >
                    <img src={icons.troop} />
                </button>
            )}

            {showBuildingMenu && (
                <BuildingMenu
                    buildings={buildings}
                    setShowBuildingMenu={setShowBuildingMenu}
                    setSelectedElement={setSelectedElement}
                    selectedElement={selectedElement}
                    selectedBuilding={selectedBuilding}
                    setSelectedBuilding={setSelectedBuilding}
                />
            )}

            {showTroopsMenu && (
                <TroopsMenu
                    setShowTroopsMenu={setShowTroopsMenu}
                    setSelectedElement={setSelectedElement}
                    selectedElement={selectedElement}
                />
            )}

            <div className={"flex " + (showBuildingMenu ? "pl-96" : "") + (showTroopsMenu ? "pr-96" : "")}>
                <div
                    className={
                        "flex flex-col justify-between " + (!showTroopsMenu && !showBuildingMenu ? "mr-12" : "mr-4")
                    }
                >
                    <div className="flex flex-col items-center mt-20">
                        <h2 className="text-white text-3xl">{displayOwnState ? "Your" : "Opponent's"} Bonuses</h2>
                        <div className="grid grid-cols-2 mt-2">
                            {getState() && getState().playerBonuses.map((bonus) => (
                                <div key={bonus.bonusType} className="flex items-center">
                                    <img src={BONUS_ICONS[bonus.bonusType]} className="mr-4" />
                                    <h2 className="text-white text-4xl mr-4">{bonus.number}</h2>
                                </div>
                            ))}
                        </div>
                    </div>

                    <div className="flex flex-col items-center">
                        <h2 className="text-white text-3xl">{displayOwnState ? "Your" : "Opponent's"} Resources</h2>
                        <div className="grid grid-cols-2 mt-2">
                            {getState() && getState().playerResources.map((resource) => (
                                <div key={"resource" + resource.element} className="flex items-center">
                                    <img className="w-20 mr-2" src={ELEMENT_ICONS[resource.element]} />
                                    <h2 className="text-white text-4xl mr-2">{resource.number}</h2>
                                </div>
                            ))}
                        </div>
                    </div>
                </div>

                <div className="grid grid-cols-5 mt-12">
                    {Array(25).fill().map((_, i) => (
                        <div key={i}
                            onClick={() => placeOnBoard(i)}
                            onMouseEnter={() => setHoverSpace(i)}
                            onMouseLeave={() => setHoverSpace(null)}
                            className="w-28 h-28 border-black border-2 flex flex-col justify-center">
                            {getState() && getState().playerBuildings.filter(building => building.boardSpace == i).length > 0 && <img src={buildingImages[buildings.filter(building => building.identifier == getState().playerBuildings.filter(building => building.boardSpace == i)[0].buildingIdentifier)[0].image]} />}
                            {!!(!(getState() && getState().playerBuildings.filter(building => building.boardSpace == i).length > 0) && selectedBuilding != null && hoverSpace != null && hoverSpace == i) && <img src={buildingImages[buildings.filter(building => building.identifier == selectedBuilding)[0].image]} />}
                        </div>
                    ))}
                </div>
                <div
                    className={
                        "flex flex-col justify-between " + (!showTroopsMenu && !showBuildingMenu ? "ml-12" : "ml-4")
                    }
                >
                    <div className="flex flex-col items-center mt-20">
                        <img
                            src={icons.eye}
                            className="w-16 h-16"
                            onMouseEnter={(e) => (e.currentTarget.src = icons.eye_h)}
                            onMouseLeave={(e) => (e.currentTarget.src = icons.eye)}
                        />
                        <img
                            src={displayOwnState ? icons.u_arrow : icons.d_arrow}
                            className="w-16 h-16 mt-8"
                            onClick={() => setDisplayOwnState(curr => !curr)}
                            onMouseEnter={(e) => (e.currentTarget.src = displayOwnState ? icons.u_arrow_h : icons.d_arrow_h)}
                            onMouseLeave={(e) => (e.currentTarget.src = displayOwnState ? icons.u_arrow : icons.d_arrow)}
                        />
                    </div>
                    <div className="flex flex-col items-center">
                        <h2 className="text-white text-3xl">{displayOwnState ? "Your" : "Opponent's"} Production</h2>
                        <div className="grid grid-cols-2 mt-2">
                            {getState() && getState().playerProduction.map((production) => (
                                <div key={"production" + production.element} className="flex items-center">
                                    <img className="w-20 mr-2" src={ELEMENT_ICONS[production.element]} />
                                    <h2 className="text-white text-4xl mr-2">{production.number}</h2>
                                </div>
                            ))}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Game;
