import icons from "../assets/icons/icon";
import elements from "../assets/elements/elements";
import ElementCard from "./ElementCard";

function TroopsMenu({setShowTroopsMenu, setSelectedElement, selectedElement}) {
    return (
        <div className="absolute bg-stone-700 w-96 h-screen right-0">
            <div className="mt-20 flex items-center px-8 py-4">
                <img src={icons.troop} onClick={() => setShowTroopsMenu(false)} />
                <h2 className="text-white text-4xl pl-4">Troops</h2>
                {selectedElement && (
                    <img
                        className="h-16 pl-4 cursor-pointer"
                        onClick={() => setSelectedElement("")}
                        src={elements[selectedElement]}
                    />
                )}
            </div>
            {!selectedElement && (
                <div className="grid grid-cols-2 gap-x-1 gap-y-4 px-5 pt-2">
                    {["Fire", "Water", "Earth", "Air"].map((element) => (
                        <ElementCard
                            key={element}
                            setSelectedElement={setSelectedElement}
                            title={element}
                            image={elements[element.toUpperCase()]}
                        />
                    ))}
                </div>
            )}
           
        </div>
    );
}

export default TroopsMenu;
