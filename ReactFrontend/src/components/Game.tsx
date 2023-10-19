import React, { useState } from "react";
import { Observer } from "mobx-react";
import { AttackModal } from "./AttackModal";
import { Button } from "./shared/Button";
import { BOARD_MAPPING } from "../utils/constants";
import { BoardStore } from "../stores/BoardStore";
import { classIconMap } from "../assets/icons/ClassIcons";

export const BOARD = Array(161 + 10).fill("");

export const Game = () => {
  const [showModal, setShowModal] = useState(false);

  const boardStore = new BoardStore();

  return (
    <Observer>{() =>
      <div>
        {showModal && <AttackModal closeModal={() => setShowModal(false)} />}
        <div className="grid grid-cols-6">
          <div className="flex justify-evenly col-span-1 mt-2">
            <div>
              <Button text="Attack" onClick={() => setShowModal(true)} />
            </div>
            <div>
              <Button text="Move" onClick={() => boardStore.move()} />
            </div>
            <div>
              <Button text="Get Game State" onClick={() => boardStore.getGameState()} />
            </div>
          </div>
          <div className="main flex mt-6 col-span-4">
            <div className="container">
              {
                BOARD.map((x, i) => BOARD_MAPPING[i]).map((position, i) => {
                  const occupant = boardStore.getTileOccupant(position);
                  return <div
                    key={i}
                    className={`caret-transparent ${boardStore.getTileColor(position)}`}
                    onClick={() => { boardStore.selectTile(position) }}
                  >
                    {occupant && <img src={classIconMap[occupant.class]} alt={occupant.toString()}/>}
                  </div>
                })
              }
            </div>
          </div>
          <div className="col-span-1">
          </div>
        </div>
      </div>
    }</Observer>
  );
};