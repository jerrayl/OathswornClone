import { observer } from "mobx-react";
import { Button } from "../Game/components/shared/Button";
import { NewPlayerForm } from "./MainMenuStore";
import { classIconMap } from "../assets/icons/ClassIcons";
import { Class } from "../utils/apiModels";

export interface NewPlayerModalProps {
    form: NewPlayerForm;
    closeModal: () => void;
    submitForm: () => void;
}

export const NewPlayerModal = observer(({ form, closeModal, submitForm }: NewPlayerModalProps) => {
    return (
        <div className="font-serif fixed z-10 inset-0 flex items-center justify-center min-h-full bg-stone-800 bg-opacity-40">
            <div className="rounded-lg overflow-hidden shadow-xl max-w-lg min-w-[25%] bg-stone-400 px-6 py-6 text-center">
                <div className="flex justify-between mb-4">
                    <h3 className="text-xl font-medium">
                        New Player
                    </h3>
                    <h3
                        className="text-xl font-sans cursor-pointer font-medium -mt-1"
                        onClick={closeModal}>
                        x
                    </h3>
                </div>
                <div className="grid grid-cols-6 gap-2 p-5">
                    {Object.keys(Class).map(x => x as any as Class).map(cls => 
                        <div className={`border border-transparent hover:border-stone-500 rounded h-12 w-12 ${cls === form.class ? "bg-stone-500" : ""}`} onClick={() => form.class = cls}>
                            <img className="" src={classIconMap[cls as any as Class]} alt={cls.toString()} />
                        </div>
                    )}
                </div>
                <input 
                    type="text" 
                    id="name" 
                    className="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:border-gray-500 w-full p-2.5 outline-none" 
                    placeholder="Name"
                    value={form.name}
                    onChange={e => form.name = e.target.value}
                    required 
                />

                <div>
                    <div className="flex justify-center mt-4">
                        <Button text="Create Player" onClick={submitForm} />
                    </div>
                </div>
            </div>
        </div>
    );
});
