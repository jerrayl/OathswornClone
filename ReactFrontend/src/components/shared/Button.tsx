export interface ButtonProps {
    text: string;
    onClick: () => void
}

export const Button = ({text, onClick}: ButtonProps) => {
    return (
        <button
            type="button"
            className="inline-flex justify-center rounded-md shadow-sm px-6 py-2 bg-stone-600 text-white font-medium hover:bg-stone-700 ml-3 w-auto text-sm caret-transparent"
            onClick={onClick}
        >
            {text}
        </button>
    )
}