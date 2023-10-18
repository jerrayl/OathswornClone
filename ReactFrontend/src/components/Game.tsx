import { useState } from "react";
import { AttackModal } from "./AttackModal";
import { Button } from "./shared/Button";
import { BOARD_MAPPING } from "../utils/constants";

export const BOARD = Array(161 + 10).fill([]);

export const Game = () => {
  const [showModal, setShowModal] = useState(false);

  return (
    <div>
      {showModal && <AttackModal closeModal={() => setShowModal(false)} />}
      <div className="grid grid-cols-6">
        <div className="flex justify-evenly col-span-1 mt-2">
          <div>
            <Button text="Attack" onClick={() => setShowModal(true)} />
          </div>
        </div>
        <div className="main flex mt-6 col-span-4">
          <div className="container">
            {
              BOARD.map((x, i) => <div key={i} className="caret-transparent flex justify-center align-center pl-10 pt-6 text-2xl">{BOARD_MAPPING[i] !== "x" ? String(BOARD_MAPPING[i]) : i}</div>)
            }
          </div>
        </div>
        <div className="col-span-1">
        </div>
      </div>
    </div>
  );
};