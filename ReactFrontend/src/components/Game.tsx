import { observer } from "mobx-react";
import { AttackModal } from "./AttackModal";
import { MoveModal } from "./MoveModal";
import { Button } from "./shared/Button";
import { BOARD_MAPPING } from "../utils/constants";
import { BoardStore } from "../stores/BoardStore";
import { PlayerSummary } from "./PlayerSummary";
import { BossSummary } from "./BossSummary";

export const BOARD = Array(161 + 10).fill("");

export interface GameProps {
  boardStore: BoardStore;
}

export const Game = observer(({ boardStore }: GameProps) => {
  return (
    <div>
      {boardStore.attackStore && <AttackModal attackStore={boardStore.attackStore} closeModal={() => boardStore.attackStore = null} />}
      {boardStore.pendingMove && <MoveModal cost={boardStore.selectedPath.length} move={boardStore.move} closeModal={boardStore.cancelMove} />}
      <div className="grid grid-cols-10 caret-transparent">
        <div className="flex flex-col justify-evenly col-span-2 mt-2">
          <div className="flex justify-around">
            <Button text="Attack" onClick={() => boardStore.attack()} />
            <Button text="End Turn" onClick={() => boardStore.getGameState()} />
          </div>
          <BossSummary boss={boardStore.getGameState().boss}/>
        </div>
        <div className="main flex mt-1 ml-4 col-span-6">
          <div className="container">
            {
              BOARD.map((x, i) => BOARD_MAPPING[i]).map((position, i) => {
                const occupant = boardStore.getTileOccupant(position);
                return <div
                  key={i}
                  className={boardStore.getTileColor(position)}
                  onClick={() => { boardStore.selectTile(position) }}
                >
                  {occupant && <img className="w-14 h-14 mt-2 ml-2" src={occupant.content} alt={occupant.description} />}
                </div>
              })
            }
          </div>
        </div>
        <div className="col-span-2 mt-2 mr-4 flex flex-col gap-4">
          {boardStore.getGameState().players.map(x => <PlayerSummary key={x.class} player={x} isSelected={x.id === boardStore.selectedPlayerId} />)}
        </div>
      </div>
    </div>
  );
});