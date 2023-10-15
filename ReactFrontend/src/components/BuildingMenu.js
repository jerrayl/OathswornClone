import icons from "../assets/icons/icon";
import elements from "../assets/elements/elements";
import ElementCard from "./ElementCard";
import BuildingCard from "./BuildingCard";
import buildingImages from "../assets/buildings/buildingImages";

function BuildingMenu({ buildings, setShowBuildingMenu, setSelectedElement, selectedElement, selectedBuilding, setSelectedBuilding}) {
    return (
        <div className="absolute bg-stone-700 w-96 h-screen left-0 overflow-y-scroll scrollbar scrollbar-thin scrollbar-thumb-stone-500">
            <div className="mt-20 flex items-center px-8 py-4">
                <img src={icons.building} onClick={() => setShowBuildingMenu(false)} />
                <h2 className="text-white text-4xl pl-4">Buildings</h2>
                {selectedElement &&
                    <img
                        className="h-16 pl-4 cursor-pointer"
                        onClick={() => setSelectedElement("")}
                        src={elements[selectedElement]}
                    />
                }
            </div>
            {!selectedElement && (
                <div className="grid grid-cols-2 gap-x-1 gap-y-4 px-5 pt-2">
                    {["Fire", "Water", "Earth", "Air", "Food", "Arcana"].map((element) => (
                        <ElementCard
                            key={element}
                            setSelectedElement={setSelectedElement}
                            title={element}
                            image={elements[element.toUpperCase()]}
                        />
                    ))}
                </div>
            )}
            {selectedElement && (
                <div className="grid grid-flow-row gap-y-4 px-5 pt-2">
                    {buildings
                        .filter((building) => building.element == selectedElement)
                        .map((building) => (
                            <BuildingCard
                                key={building.identifier}
                                identifier={building.identifier}
                                title={building.name}
                                costs={building.cost}
                                production={building.production}
                                health={building.health}
                                effect={building.effect}
                                image={buildingImages[building.image]}
                                selectedBuilding={selectedBuilding}
                                setSelectedBuilding={setSelectedBuilding}
                            />
                        ))}
                </div>
            )}
        </div>
    );
}

export default BuildingMenu;
