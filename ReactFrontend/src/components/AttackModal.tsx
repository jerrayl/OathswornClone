import chevronUp from "../assets/icons/chevron-up.svg";
import chevronDown from "../assets/icons/chevron-down.svg";
import empower from "../assets/icons/Empower.png";
import redraw from "../assets/icons/Redraw.png";
import { AttackStore } from "../stores/AttackStore";
import { Observer } from "mobx-react";
import { Might } from "../utils/apiModels";
import { MightCard } from "../utils/types";
import { Button } from "./shared/Button";

const COLORS = {
    [Might.White]: "bg-gray-100 text-gray-900",
    [Might.Yellow]: "bg-yellow-300 text-gray-900",
    [Might.Red]: "bg-red-500 text-gray-900",
    [Might.Black]: "bg-gray-900 text-gray-100"
}

const HIGHLIGHTS = {
    [Might.White]: "hover:shadow-gray-50",
    [Might.Yellow]: "hover:shadow-yellow-200",
    [Might.Red]: "hover:shadow-red-400",
    [Might.Black]: "hover:shadow-gray-800"
}

export interface MightCardDisplayProps {
    mightCard: MightCard;
    toggleCard?: () => void;
    pendingRedraw?: boolean;
}


export const MightCardDisplay = ({ mightCard, toggleCard, pendingRedraw }: MightCardDisplayProps) => {
    return (
        <div className="flex flex-col items-center">
            <div
                className={`rounded-lg shadow-sm w-12 h-12 ${COLORS[mightCard.type]} font-medium text-xl flex flex-col justify-center ${mightCard.isDrawnFromCritical ? "hover:cursor-default" : `hover:shadow-sm ${HIGHLIGHTS[mightCard.type]} hover:cursor-pointer`}`}
                onClick={toggleCard}
            >
                {pendingRedraw ? "X" : mightCard.isCritical ? `{${mightCard.value}}` : mightCard.value}
            </div>
        </div>
    )
}

export interface MightTypeSelectorProps {
    might: Might;
    value: number;
    changeValue: (increase: boolean) => void;
}

export const MightTypeSelector = ({ might, value, changeValue }: MightTypeSelectorProps) => {
    return (
        <div className="flex flex-col items-center">
            <img className="w-9 h-9 hover:cursor-pointer" src={chevronUp} alt="up" onClick={() => changeValue(true)} />
            <div className={`rounded-lg shadow-sm w-12 h-12 ${COLORS[might]} font-medium text-xl flex flex-col justify-center hover:cursor-default`}>
                {value}
            </div>
            <img className="w-9 h-9 hover:cursor-pointer" src={chevronDown} alt="down" onClick={() => changeValue(false)} />
        </div>
    )
}

export interface AttackModalProps {
    closeModal: () => void;
}

export const AttackModal = ({ closeModal }: AttackModalProps) => {

    const attackStore = new AttackStore();

    return (
        <Observer>{() =>
            <div className="font-serif fixed z-10 inset-0 flex items-center justify-center min-h-full caret-transparent bg-stone-800 bg-opacity-40">
                <div className="rounded-lg overflow-hidden shadow-xl max-w-lg min-w-[25%]">
                    <div className="bg-stone-400 px-6 py-6">
                        <div className="text-center">
                            <div className="flex justify-between">
                                <h3 className="text-xl font-medium">
                                    Attack
                                </h3>
                                <h3
                                    className="text-xl font-sans cursor-pointer font-medium -mt-1"
                                    onClick={closeModal}>
                                    x
                                </h3>
                            </div>

                            {attackStore.cardsDrawn ?
                                <div className="flex flex-col px-4 py-8">
                                    <div className="flex text-2xl justify-center">
                                        <img className="w-8 h-8" src={redraw} alt="redraw" />: {attackStore.redrawTokensUsed}
                                    </div>
                                    <div className="mt-2 font-lg text-lg">
                                        {attackStore.cardsDrawn.filter(x => x.value === 0).length >= 2 ? "Miss" : `${attackStore.cardsDrawn.filter(card => !card.isDrawnFromCritical).reduce((partialSum, card) => partialSum + card.value, 0)} damage`}
                                    </div>
                                    <div className="grid grid-cols-6 gap-4 mt-3">
                                        {attackStore.cardsDrawn.filter(x => !x.isDrawnFromCritical).map((card, i) =>
                                            <MightCardDisplay
                                                key={i}
                                                mightCard={card}
                                                toggleCard={() => attackStore.toggleCardToRedraw(card.id)}
                                                pendingRedraw={attackStore.cardsToRedraw.includes(card.id)}
                                            />)}
                                    </div>
                                    <hr className="h-2px my-6 bg-gray-700 border-0" />
                                    <div className="grid grid-cols-6 gap-4">
                                        {attackStore.cardsDrawn.filter(x => x.isDrawnFromCritical).map((card, i) =>
                                            <MightCardDisplay key={i} mightCard={card} />)}
                                    </div>
                                </div>
                                : <div className="flex flex-col justify-between px-4 py-8">
                                    <div className="flex text-2xl justify-center">
                                        <img className="w-8 h-8" src={empower} alt="empower" />: {attackStore.empowerTokensUsed}
                                    </div>
                                    <div className="flex justify-around mt-2 space-x-4">
                                        {[...attackStore.mightCards.entries()].map((might, i) =>
                                            <MightTypeSelector
                                                key={i}
                                                might={might[0]}
                                                value={might[1]}
                                                changeValue={(increase: boolean) => attackStore.mightChanged(might[0], increase)}
                                            />)}
                                    </div>
                                </div>
                            }

                            <div>
                                {attackStore.cardsDrawn ?
                                    <div className="flex justify-evenly">
                                        <Button text="Reroll" onClick={async () => await attackStore.redrawCards()} />
                                        <Button text="Complete attack" onClick={async () => {await attackStore.completeAttack();closeModal();}} />
                                    </div> :
                                    <div className="flex justify-center">
                                        <Button text="Draw cards" onClick={async () => await attackStore.drawCards()} />
                                    </div>
                                }
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        }</Observer>
    );
}
