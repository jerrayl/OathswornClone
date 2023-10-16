import { Might } from "./apiModels";

export type MightCard = {
    id: number;
    value: number;
    type: Might;
    isCritical: boolean;
    isDrawnFromCritical: boolean;
}