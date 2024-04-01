
import { classIconMap } from "../assets/icons/ClassIcons";
import { FreeCompanyModel } from "../utils/apiModels";
import { observer } from "mobx-react";

export interface FreeCompaniesProps {
    freeCompanies: FreeCompanyModel[];
    startEncounter: (code: string) => void;
}

export const FreeCompanies = observer(({ freeCompanies, startEncounter }: FreeCompaniesProps) => {
    return (
        <div className="flex flex-col items-center h-[80vh] w-full">
            <div className="text-xl font-bold">
                Free Companies
            </div>
            <div className="mt-3 overflow-auto grid md:grid-cols-2 grid-cols-1 gap-4">
                {freeCompanies.map(freeCompany =>
                    <div className="relative flex flex-col items-center rounded-md border border-gray-200 hover:border-gray-300 shadow-md">
                        <div className="grid grid-cols-6">
                            <div className="p-3 col-span-4 flex flex-col w-auto justify-center items-center">
                                <h4 className="text-xl font-bold text-navy-700">{freeCompany.name}</h4>
                                <div className="flex justify-end font-semibold hover:text-gray-700">
                                    {freeCompany.players.length === 4 && <button onClick={() => startEncounter(freeCompany.code)}>Start Encounter</button>}
                                </div>
                            </div>
                        </div>
                        <div className="flex w-auto justify-center border-t divide-x pb-2 px-2 grid grid-cols-2">
                            {freeCompany.players.map(player =>
                                <div className="grid grid-cols-3">
                                    <div className="p-3 flex justify-center items-center">
                                        <img className="w-14 h-14" src={classIconMap[player.class]} alt={player.class.toString()} />
                                    </div>
                                    <div className="p-3 col-span-2 flex flex-col w-auto justify-center items-center">
                                        <h4 className="text-xl font-bold text-navy-700">{player.name}</h4>
                                        <p className="font-dm text-sm font-medium text-gray-600">{player.class}</p>
                                        <p className="font-dm text-sm font-medium text-gray-600">{player.userEmail}</p>
                                    </div>
                                </div>
                            )}
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
});