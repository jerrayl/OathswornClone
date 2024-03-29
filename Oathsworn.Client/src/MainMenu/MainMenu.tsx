import { Observer } from "mobx-react";
import oathswornlogo from "../assets/oathswornlogo.png";
import { MainMenuStore } from "./MainMenuStore";

export const MainMenu = () => {
  const store = new MainMenuStore();

  return (
    <Observer>
    {() =>
    <div>
      <div className="flex flex-col caret-transparent">
        <div className="flex justify-center">
          <img className="h-56" src={oathswornlogo} alt="oathsworn logo" />
        </div>
        <div className="grid grid-cols-9 caret-transparent">
          <div className="col-span-2 mt-2 mr-4 flex flex-col gap-4">
            Encounters in progress
          </div>
          <div className="col-span-2 mt-2 mr-4 flex flex-col gap-4">
            Free Companies
            {store.freeCompanies.map(x => <h5>{x.name} - {x.players.map(y => <h5>{y.name} - {y.class}</h5>)} <button >Start Encounter</button></h5>)}

          </div>
          <div className="col-span-2 mt-2 mr-4 flex flex-col gap-4">
            Players
            {store.players.map(x => <h5>{x.name} - {x.class} <button onClick={() => store.createFreeCompany(x.id)}>Create Free Company</button> <button onClick={() => store.joinFreeCompany(x.id)}>Join Free Company</button></h5>)}
            <button onClick={store.createPlayer}>Create Player</button>
          </div>
        </div>
      </div>
    </div>}
    </Observer>
  );
};