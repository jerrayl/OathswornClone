import { observer } from "mobx-react";
import { NewPlayerForm } from "./MainMenuStore";
import { Modal } from "./shared/Modal";
import { classIconMap } from "../assets/icons/ClassIcons";
import { Class } from "../utils/apiModels";

export interface NewPlayerProps {
    form: NewPlayerForm;
    closeModal: () => void;
    submitForm: () => void;
}

export const NewPlayerModal = observer(({ form, closeModal, submitForm }: NewPlayerProps) => {
    return (
        <Modal title="New Player" buttonText="Create Player" closeModal={closeModal} submitForm={submitForm}>
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
        </Modal>
    );
});
