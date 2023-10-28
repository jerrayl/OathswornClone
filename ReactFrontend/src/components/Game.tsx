import React from "react";
import { Observer } from "mobx-react";
import { AttackModal } from "./AttackModal";
import { MoveModal } from "./MoveModal";
import { Button } from "./shared/Button";
import { BOARD_MAPPING } from "../utils/constants";
import { BoardStore } from "../stores/BoardStore";
import { classIconMap } from "../assets/icons/ClassIcons";
import { PlayerSummary } from "./PlayerSummary";

export const BOARD = Array(161 + 10).fill("");

export const Game = () => {
  const boardStore = new BoardStore();

  return (
    <Observer>{() =>
      <div>
        {boardStore.attackStore && <AttackModal attackStore={boardStore.attackStore} closeModal={() => boardStore.attackStore = null} />}
        {boardStore.pendingMove && <MoveModal cost={boardStore.selectedPath.length} move={boardStore.move} closeModal={boardStore.cancelMove}/>}
        <div className="grid grid-cols-10 caret-transparent">
          <div className="flex justify-evenly col-span-2 mt-2">
            <div>
              <Button text="Attack" onClick={() => boardStore.attack()} />
            </div>
            <div>
              <Button text="Get Game State" onClick={() => boardStore.getGameState()} />
            </div>
          </div>
          <div className="main flex mt-1 col-span-6">
            <div className="container">
              {
                BOARD.map((x, i) => BOARD_MAPPING[i]).map((position, i) => {
                  const occupant = boardStore.getTileOccupant(position);
                  return <div
                    key={i}
                    className={boardStore.getTileColor(position)}
                    onClick={() => { boardStore.selectTile(position) }}
                  >
                    {occupant && <img className="w-14 h-14 mt-1 ml-1" src={classIconMap[occupant.class]} alt={occupant.class.toString()}/>}
                  </div>
                })
              }
            </div>
          </div>
          <div className="col-span-2 mt-2 mr-4 flex flex-col gap-4">
            {boardStore.players.map(x => <PlayerSummary key={x.class} player={x} isSelected={x.id === boardStore.selectedPlayerId}/>)}
          </div>
        </div>
      </div>
    }</Observer>
  );
};