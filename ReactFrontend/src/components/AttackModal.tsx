import { observer } from "mobx-react";
import chevronUp from "../assets/icons/chevron-up.svg";
import chevronDown from "../assets/icons/chevron-down.svg";
import { tokenIconMap } from "../assets/icons/TokenIcons";
import { AttackStore } from "../stores/AttackStore";
import { Might, Token } from "../utils/apiModels";
import { Button } from "./shared/Button";
import { COLORS, formatBossPart } from "../utils/constants";
import { MightCardDisplay } from "./shared/MightCardDisplay";

export interface MightTypeSelectorProps {
    might: Might;
    value: number;
    changeValue: (increase: boolean) => void;
}

export const MightTypeSelector = ({ might, value, changeValue }: MightTypeSelectorProps) => {
    return (
        <div className="flex flex-col items-center">
            <img className="w-9 h-9 hover:cursor-pointer" src={chevronUp} alt="up" onClick={() => changeValue(true)} />
            <div className={`rounded-lg shadow-sm w-12 h-12 ${COLORS[might]} font-bold text-2xl flex flex-col justify-center hover:cursor-default`}>
                {value}
            </div>
            <img className="w-9 h-9 hover:cursor-pointer" src={chevronDown} alt="down" onClick={() => changeValue(false)} />
        </div>
    )
}

export interface AttackModalProps {
    attackStore: AttackStore;
    bossPart: string | null;
    closeModal: () => void;
}

export const AttackModal = observer(({ attackStore, closeModal, bossPart }: AttackModalProps) => {
    return (
        <div className="font-serif fixed z-10 inset-0 flex items-center justify-center min-h-full caret-transparent bg-stone-800 bg-opacity-40">
            <div className="rounded-lg overflow-hidden shadow-xl max-w-lg min-w-[25%]">
                <div className="bg-stone-400 px-6 py-6">
                    <div className="text-center">
                        <div className="flex justify-between">
                            <h3 className="text-xl font-medium">
                                Attack {bossPart ? formatBossPart(bossPart) : ""}
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
                                    <img className="w-8 h-8" src={tokenIconMap[Token.Redraw]} alt={Token.Redraw.toString()} />: {attackStore.redrawTokensUsed}
                                </div>
                                <div className="mt-2 font-lg text-lg">
                                    {attackStore.cardsDrawn.filter(card => !card.isDrawnFromCritical && card.value === 0).length >= 2 ? "Miss" : `${attackStore.cardsDrawn.reduce((partialSum, card) => partialSum + card.value, 0)} damage`}
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
                                    <img className="w-8 h-8" src={tokenIconMap[Token.Empower]} alt={Token.Empower.toString()} />: {attackStore.empowerTokensUsed}{attackStore.leftoverEmpowerPoints && "*"}
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
                                    <Button text="Complete attack" onClick={async () => { await attackStore.completeAttack(); closeModal(); }} />
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
    );
});
