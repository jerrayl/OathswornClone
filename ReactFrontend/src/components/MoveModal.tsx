import { observer } from "mobx-react";
import { Button } from "./shared/Button";

export interface MoveModalProps {
    cost: number;
    move: () => Promise<void>;
    closeModal: () => void;
}

export const MoveModal = observer(({ cost, move, closeModal }: MoveModalProps) => {
    return (
        <div className="font-serif fixed z-10 inset-0 flex items-center justify-center min-h-full bg-stone-800 bg-opacity-40">
            <div className="rounded-lg overflow-hidden shadow-xl max-w-lg min-w-[25%] bg-stone-400 px-6 py-6 text-center">
                <div className="flex justify-between mb-4">
                    <h3 className="text-xl font-medium">
                        Move
                    </h3>
                    <h3
                        className="text-xl font-sans cursor-pointer font-medium -mt-1"
                        onClick={closeModal}>
                        x
                    </h3>
                </div>

                <h2 className="text-lg font-medium">
                    Cost: {cost} Animus
                </h2>

                <div>
                    <div className="flex justify-center mt-4">
                        <Button text="Confirm" onClick={async () => await move()} />
                    </div>
                </div>
            </div>
        </div>
    );
});
