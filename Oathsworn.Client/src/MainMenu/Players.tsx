
import { classIconMap } from "../assets/icons/ClassIcons";
import { PlayerSummaryModel } from "../utils/apiModels";
import { observer } from "mobx-react";

export interface PlayersProps {
    players: PlayerSummaryModel[];
    showNewFreeCompanyModal: (player : PlayerSummaryModel) => void;
    showJoinFreeCompanyModal: (player : PlayerSummaryModel) => void;
    showNewPlayerModal: () => void;
}

export const Players = observer(({ players, showNewFreeCompanyModal, showJoinFreeCompanyModal, showNewPlayerModal }: PlayersProps) => {
    return (
        <div className="flex flex-col items-center h-[80vh] w-full">
            <div className="grid grid-cols-3 w-full">
                <div className="text-xl font-bold col-start-2 flex justify-center">
                    Players
                </div>
                <div className="flex justify-end font-semibold hover:text-gray-700">
                    <button onClick={showNewPlayerModal}>Create Player</button>
                </div>
            </div>
            <div className="mt-3 overflow-auto grid md:grid-cols-2 grid-cols-1 gap-4">
                {players.map((player, i) =>
                    <div key={`player${i}`} className="relative flex flex-col items-center rounded-md border border-gray-200 hover:border-gray-300 shadow-md">
                        <div className="grid grid-cols-6">
                            <div className="p-3 flex justify-center items-center col-span-2">
                                <img className="w-14 h-14" src={classIconMap[player.class]} alt={player.class.toString()} />
                            </div>
                            <div className="p-3 col-span-4 flex flex-col w-auto justify-center items-center">
                                <h4 className="text-xl font-bold text-navy-700">{player.name}</h4>
                                <p className="font-dm text-sm font-medium text-gray-600">{player.class}</p>
                            </div>
                        </div>
                        <div className="flex w-auto justify-center border-t divide-x py-1 px-2">
                            <button className="font-semibold text-sm pr-2 py-1 hover:text-gray-700" onClick={() => showNewFreeCompanyModal(player)}>Create Free Company</button>
                            <button className="font-semibold text-sm pl-2 py-1 hover:text-gray-700" onClick={() => showJoinFreeCompanyModal(player)}>Join Free Company</button>
                        </div>
                    </div>
                )}
            </div>
        </div>
    );
});