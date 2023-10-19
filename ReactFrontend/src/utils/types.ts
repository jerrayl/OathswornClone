import { Class, Might, Token } from "./apiModels";

export type MightCard = {
    id: number;
    value: number;
    type: Might;
    isCritical: boolean;
    isDrawnFromCritical: boolean;
}

export type Player = {
    id: number;
    class: Class;
    defence: number;
    maxAnimus: number;
    animusRegen: number;
    might: {[key in Might]: number};
    xPosition: number;
    yPosition: number;
    currentHealth: number;
    currentAnimus: number;
    tokens: {[key in Token]: number};
}