import { CharacterType, Might } from "./apiModels";

export type TileOccupant = {
    id: number;
    type: CharacterType;
    content: string;
    description: string;
}

export type MightCard = {
    id: number;
    value: number;
    type: Might;
    isCritical: boolean;
    isDrawnFromCritical: boolean;
}