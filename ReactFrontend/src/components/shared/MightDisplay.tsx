import { COLORS } from "../../utils/constants";

export interface MightDisplayProps {
    className: string;
    might: { [key: string]: number; };
    length: number;
}

export const MightDisplay = ({ className, might, length }: MightDisplayProps) => {
    return (
        <div className={className}>
            {
                // Multiply each might type by its number e.g. ["Black", "Red", "Red"], and pad to 4
                Array.from({ ...Object.entries(might).map(x => Array(x[1]).fill(x[0])).flat(), length: length }).map(x =>
                    <div className={`shadow-sm w-6 h-6 ${x ? COLORS[x] : "bg-grey-100"} justify-center`} />
                )}
        </div>
    )
}