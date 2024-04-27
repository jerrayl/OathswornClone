import { observer } from "mobx-react";
import { NewFreeCompanyForm } from "./MainMenuStore";
import { Modal } from "./shared/Modal";

export interface NewFreeCompanyProps {
    form: NewFreeCompanyForm;
    closeModal: () => void;
    submitForm: () => void;
}

export const NewFreeCompanyModal = observer(({ form, closeModal, submitForm }: NewFreeCompanyProps) => {
    return (
        <Modal title="New Free Company" buttonText="Create Free Company" closeModal={closeModal} submitForm={submitForm}>
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
