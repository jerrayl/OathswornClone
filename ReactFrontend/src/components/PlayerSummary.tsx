import React from "react";
import { observer } from "mobx-react";
import { classIconMap } from "../assets/icons/ClassIcons";
import { tokenIconMap } from "../assets/icons/TokenIcons";
import { Player } from "../utils/types";
import { Token } from "../utils/apiModels";
import { COLORS } from "../utils/constants";

export interface PlayerSummaryProps {
  player: Player;
  isSelected: boolean;
}

export const PlayerSummary = observer(({ player, isSelected }: PlayerSummaryProps) => {
  return (
    <div className={`rounded-lg overflow-hidden shadow-xl text-center px-2 py-2 grid grid-cols-12 caret-transparent ${isSelected ? "bg-gray-400" : "bg-zinc-400"}`}>
      <div className="col-span-3 flex flex-col justify-center">
        <img src={classIconMap[player.class]} alt={player.class.toString()} />
      </div>
      <div className="col-span-9 flex flex-col gap-2">
        <div className="flex justify-evenly">
          <div className="rounded-lg shadow-md bg-rose-800 text-amber-300 w-12 h-12 justify-center font-medium text-xl flex items-center">
            {player.currentHealth}
          </div>
          <div className="defense rounded-lg shadow-md bg-sky-300 text-zinc-500 w-12 h-12 justify-center font-medium text-xl pt-1">
            {player.defence}
          </div>
          <div className="animus rounded-lg shadow-md bg-amber-400 text-yellow-300 font-outline-1 w-12 h-12 justify-center font-bold text-xl pt-2">
            {player.currentAnimus}
          </div>
        </div>
        <div className="flex justify-center gap-2">
          {
            // Multiply each might type by its number e.g. ["Black", "Red", "Red"], and pad to 4
            Array.from({...Object.entries(player.might).map(x => Array(x[1]).fill(x[0])).flat().reverse(), length:4}).map(x =>
            <div className={`shadow-sm w-6 h-6 ${x ? COLORS[x] : "bg-grey-100"} justify-center`} />
          )}
        </div>
        <div className="flex text-xl font-medium justify-evenly">
          {Object.entries(player.tokens).map(entry => <div>{<img className="h-8 w-8" src={tokenIconMap[entry[0] as unknown as Token]} />}{entry[1]}</div>)}
        </div>
      </div>
    </div >
  );
});