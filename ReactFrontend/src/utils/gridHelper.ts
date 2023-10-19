import { Position } from "./apiModels";

export const Equal = (position1: Position | null, position2: Position | null): boolean => {
    return !!position1 && !!position2 && position1.xPosition === position2.xPosition && position1.yPosition === position2.yPosition;
}

export const GetDistanceAlongAxis = (position1: Position, position2: Position): Number | null => {
    if (!IsOnSameAxis(position1, position2)) {
        return null;
    }

    return Math.max(Math.abs(position1.xPosition - position2.xPosition), Math.abs(position1.yPosition - position2.yPosition));
}

export const IsOnSameAxis = (position1: Position, position2: Position): boolean => {
    return (position1.xPosition === position2.xPosition) ||
        (position1.yPosition === position2.yPosition) ||
        Math.abs(position1.xPosition - position2.xPosition) == Math.abs(position1.yPosition - position2.yPosition) && (position1.xPosition - position2.xPosition) * (position1.yPosition - position2.yPosition) < 0;
}

export const IsValidPath = (positions: Position[]): boolean => {
    return positions.length > 2 &&
        positions.reduce((prev, curr, i) => prev && i === 0 || IsAdjacent(positions[i - 1], curr), true);
}

export const IsAdjacent = (position1: Position, position2: Position): boolean => {
    return (position1.xPosition == position2.xPosition && Math.abs(position1.yPosition - position2.yPosition) === 1) ||
        (position1.yPosition == position2.yPosition && Math.abs(position1.xPosition - position2.xPosition) === 1) ||
        (position1.xPosition - position2.xPosition) * (position1.yPosition - position2.yPosition) === -1;
}