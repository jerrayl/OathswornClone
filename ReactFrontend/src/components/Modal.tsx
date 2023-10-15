import chevronUp from "../assets/icons/chevron-up.svg";
import chevronDown from "../assets/icons/chevron-down.svg";
import { Might } from "../utils/constants";
import { AttackStore } from "../stores/AttackStore";
import { Observer } from "mobx-react";

export interface MightTypeSelectorProps {
    might: Might;
    value: number;
    changeValue: (increase: boolean) => void;
}

export const MightTypeSelector = ({ might, value, changeValue }: MightTypeSelectorProps) => {
    const colors = {
        [Might.White]: "bg-gray-100 text-gray-900",
        [Might.Yellow]: "bg-yellow-300 text-gray-900",
        [Might.Red]: "bg-red-500 text-gray-900",
        [Might.Black]: "bg-gray-900 text-gray-100"
    }

    return (
        <div className="flex flex-col items-center">
            <img className="w-9 h-9 hover:cursor-pointer" src={chevronUp} alt="up" onClick={() => changeValue(true)} />
            <div className={`rounded-lg shadow-sm px-6 py-4 ${colors[might]} w-auto font-medium text-xl caret-transparent`}>
                {value}
            </div>
            <img className="w-9 h-9 hover:cursor-pointer" src={chevronDown} alt="down" onClick={() => changeValue(false)} />
        </div>
    )
}

function Modal() {

    const attackStore = new AttackStore();

    return (
        <Observer>{() => 
            <div className="font-serif fixed z-10 inset-0 flex items-center justify-center min-h-full caret-transparent">
                <div className="rounded-lg overflow-hidden shadow-xl max-w-lg">
                    <div className="bg-stone-400 px-4 py-5">
                        <div className="text-center mx-2">
                            <div className="flex justify-between">
                                <h3 className="text-xl font-medium text-white">
                                    Attack
                                </h3>
                                <h3 className="text-xl font-sans cursor-pointer font-medium text-white -mt-2">x</h3>
                            </div>
                            <div className="mt-2 flex space-x-4 justify-between">
                                {[...attackStore.mightCards.entries()].map((might, i) => <MightTypeSelector key={i} might={might[0]} value={might[1]} changeValue={(increase: boolean) => attackStore.mightChanged(might[0], increase)} />)}
                            </div>
                        </div>
                    </div>
                    <div className="bg-stone-500 py-3 px-6 flex flex-row-reverse">
                        <button
                            type="button"
                            className="w-full inline-flex justify-center rounded-md shadow-sm px-4 py-2 bg-stone-600 text-white font-medium hover:bg-stone-800 ml-3 w-auto text-sm"
                        >
                            Accept
                        </button>
                    </div>
                </div>
            </div>
        }</Observer>
    );
}

export default Modal;
