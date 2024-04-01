import { Observer } from "mobx-react";
import oathswornlogo from "../assets/oathswornlogo.png";
import { MainMenuStore } from "./MainMenuStore";
import { Players } from "./Players";
import { FreeCompanies } from "./FreeCompanies";
import { NewPlayerModal } from "./NewPlayerModal";

export const MainMenu = () => {
  const store = new MainMenuStore();

  return (
    <Observer>
      {() =>
        <div>
          {store.newPlayerForm && <NewPlayerModal form={store.newPlayerForm} closeModal={() => store.newPlayerForm = null} submitForm={store.createPlayer}/>}
          <div className="flex flex-col caret-transparent">
            <div className="flex justify-center h-[20vh]">
              <img className="h-full" src={oathswornlogo} alt="oathsworn logo" />
            </div>
            <div className="grid grid-cols-9 caret-transparent">
              <div className="col-span-3 mt-2 mr-4 flex flex-col gap-4">
                Encounters in progress
              </div>
              <div className="col-span-3">
                <FreeCompanies freeCompanies={store.freeCompanies} startEncounter={store.startEncounter} />
              </div>
              <div className="col-span-3 mr-4">
                <Players players={store.players} createFreeCompany={store.createFreeCompany} joinFreeCompany={store.joinFreeCompany} showNewPlayerModal={store.showNewPlayerModal} />
              </div>
            </div>
          </div>
        </div>}
    </Observer>
  );
};