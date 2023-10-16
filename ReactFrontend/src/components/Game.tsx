import { useState } from "react";
import { AttackModal } from "./AttackModal";
import { Button } from "./shared/Button";

export const BOARD = Array(161).fill([]);

export const Game = () => {
  const [showModal, setShowModal] = useState(false);

  return (
    <div>
      {showModal && <AttackModal closeModal={() => setShowModal(false)} />}
      <div className="flex justify-evenly mt-2">
        <Button text="Attack" onClick={() => setShowModal(true)} />
      </div>
      <div className="main flex">
        <div className="container">
          {
            BOARD.map(x => <div />)
          }
        </div>
      </div>

    </div>
  );
};