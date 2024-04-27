import { observer } from "mobx-react";
import { JoinFreeCompanyForm } from "./MainMenuStore";
import { Modal } from "./shared/Modal";

export interface NewFreeCompanyProps {
    form: JoinFreeCompanyForm;
    closeModal: () => void;
    submitForm: () => void;
}

export const JoinFreeCompanyModal = observer(({ form, closeModal, submitForm }: NewFreeCompanyProps) => {
    return (
        <Modal title="Join Free Company" buttonText="Join Free Company" closeModal={closeModal} submitForm={submitForm}>
            <input
                type="text"
                id="code"
                className="bg-gray-50 border border-gray-300 text-gray-900 rounded-lg focus:border-gray-500 w-full p-2.5 outline-none"
                placeholder="Code"
                value={form.code}
                onChange={e => form.code = e.target.value}
                required
            />
        </Modal>
    );
});
