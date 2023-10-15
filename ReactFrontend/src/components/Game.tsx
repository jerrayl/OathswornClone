import { useState } from "react";
import Modal from "./Modal";

export const BOARD = Array(161).fill([]);

export const Game = () => {
  const [showModal, setShowModal] = useState(false);

  return (
    <div>
      {showModal && <Modal/>}
      <div className="flex justify-evenly mt-2">
        <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded caret-transparent" onClick={() => setShowModal(true)}>
          Attack
        </button>
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