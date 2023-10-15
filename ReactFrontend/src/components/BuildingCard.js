import icons from "../assets/icons/icon";
import elements from "../assets/elements/elements";

function BuildingCard({ title, identifier, image, costs, production, health, effect, setSelectedBuilding, selectedBuilding }) {
    return (
        <div
            onClick={() => setSelectedBuilding(identifier)} 
            className={(selectedBuilding == identifier ? "bg-green-400 " : "bg-stone-200 hover:bg-stone-400") + "h-52 cursor-pointer rounded-lg shadow-lg grid grid-cols-7 text-stone-700"}>
            <div className="flex col-span-4 justify-center items-center">
                <img className="w-40" src={image} />
            </div>

            <div className="col-span-3 grid grid-cols-2 pr-2 mt-4 -ml-1">
                <div className="flex items-center">
                    <img src={icons.bricks_d} className="h-10 w-10 mr-1" />
                    <h2 className="text-4xl mr-4">{health}</h2>
                </div>
                <div className="flex items-center">
                    <img src={icons.production_d} className="h-10 w-10 mr-1" />
                    <h2 className="text-4xl mr-4">{production}</h2>
                </div>
                {costs.map((cost) => (
                    <div key={title + cost.element} className="flex items-center">
                        <img src={elements[cost.element]} className="h-10 w-10 mr-1" />
                        <h2 className="text-4xl mr-4">{cost.number}</h2>
                    </div>
                ))}
            </div>
            <div className="flex col-span-1" />
            <div className="flex col-span-5 justify-center items-center">
                <h2 className="text-3xl pb-1">{title}</h2>
            </div>
            <div className="flex col-span-1 justify-center items-center">
                {effect && <img src={icons.tag_d} className="h-12 w-12 mr-4 pb-2" />}
            </div>
        </div>
    );
}

export default BuildingCard;
