
import { EncounterModel } from "../utils/apiModels";
import { observer } from "mobx-react";

export interface EncountersProps {
    encounters: EncounterModel[];
    continueEncounter: (encounterId: number) => void;
}

export const Encounters = observer(({ encounters, continueEncounter }: EncountersProps) => {
    return (
        <div className="flex flex-col items-center h-[80vh] w-full">
            <div className="text-xl font-bold">
                Encounters in progress
            </div>
            <div className="mt-3 overflow-auto grid grid-cols-1 gap-4">
                {encounters.map(encounter =>
                    <div className="relative flex flex-col items-center rounded-md border border-gray-200 hover:border-gray-300 shadow-md">
                        <div className="p-3 flex justify-between w-full">
                            <div />
                            <h4 className="text-xl font-bold text-navy-700 justify-self-center">{encounter.encounterNumber}</h4>
                            <div className="font-semibold justify-self-end">
                                <button className="hover:text-gray-700" onClick={() => continueEncounter(encounter.encounterId)}>Continue Encounter</button>
                            </div>
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
});