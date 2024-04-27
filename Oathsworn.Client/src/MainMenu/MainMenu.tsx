import { Observer } from "mobx-react";
import oathswornlogo from "../assets/oathswornlogo.png";
import { MainMenuStore } from "./MainMenuStore";
import { Players } from "./Players";
import { FreeCompanies } from "./FreeCompanies";
import { NewPlayerModal } from "./NewPlayerModal";
import { NewFreeCompanyModal } from "./NewFreeCompanyModal";
import { JoinFreeCompanyModal } from "./JoinFreeCompanyModal";
import { Encounters } from "./Encounters";
import { StartEncounterModal } from "./StartEncounterModal";

export const MainMenu = () => {
  const store = new MainMenuStore();

  return (
    <Observer>
      {() =>
        <div>
          {store.newPlayerForm && <NewPlayerModal form={store.newPlayerForm} closeModal={() => store.newPlayerForm = null} submitForm={store.createPlayer}/>}
          {store.newFreeCompanyForm && <NewFreeCompanyModal form={store.newFreeCompanyForm} closeModal={() => store.newFreeCompanyForm = null} submitForm={store.createFreeCompany}/>}
          {store.joinFreeCompanyForm && <JoinFreeCompanyModal form={store.joinFreeCompanyForm} closeModal={() => store.joinFreeCompanyForm = null} submitForm={store.joinFreeCompany}/>}
          {store.startEncounterForm && <StartEncounterModal form={store.startEncounterForm} closeModal={() => store.startEncounterForm = null} submitForm={store.startEncounter}/>}

          <div className="flex flex-col caret-transparent">
            <div className="flex justify-center h-[20vh]">
              <img className="h-full" src={oathswornlogo} alt="oathsworn logo" />
            </div>
            <div className="grid grid-cols-9 caret-transparent">
              <div className="col-span-3 mt-2 mr-4 flex flex-col gap-4">
                <Encounters encounters={store.encounters} continueEncounter={store.continueEncounter}/>
              </div>
              <div className="col-span-3">
                <FreeCompanies freeCompanies={store.freeCompanies} startEncounter={store.showStartEncounterModal} />
              </div>
              <div className="col-span-3 mr-4">
                <Players players={store.players} showNewFreeCompanyModal={store.showNewFreeCompanyModal} showJoinFreeCompanyModal={store.showJoinFreeCompanyModal} showNewPlayerModal={store.showNewPlayerModal} />
              </div>
            </div>
          </div>
        </div>}
    </Observer>
  );
};