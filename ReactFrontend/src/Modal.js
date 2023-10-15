import { useState } from "react";

function Modal({ sender, setShowModal, sendRequest, acceptRequest }) {
    const [receiver, setReceiver] = useState("");

    return (
        <div className="font-serif fixed z-10 inset-0 flex items-center justify-center min-h-full">
            <div className="rounded-lg overflow-hidden shadow-xl max-w-lg">
                <div className="bg-stone-700 px-4 py-5">
                    <div className="text-center mx-2">
                        <div className="flex justify-between">
                            <h3 className="text-xl font-medium text-white">
                                Game invitation
                            </h3>
                            <h3 className="text-xl font-sans cursor-pointer font-medium text-white -mt-2" onClick={() => setShowModal(false)}>x</h3>
                        </div>
                        <div className="mt-2">
                            {sender ?
                                <p className="text-white">{sender} has invited you to begin a game</p> :
                                <>
                                    <p className="text-white">Enter an email to invite someone to play a game</p>
                                    <input className="mt-4 bg-stone-600 p-2 rounded-md w-full outline-none text-stone-200" value={receiver} onChange={e => setReceiver(e.target.value)} />
                                </>
                            }
                        </div>
                    </div>
                </div>
                <div className="bg-stone-600 py-3 px-6 flex flex-row-reverse">
                    <button
                        type="button"
                        className="w-full inline-flex justify-center rounded-md shadow-sm px-4 py-2 bg-stone-700 text-white font-medium hover:bg-stone-800 ml-3 w-auto text-sm"
                        onClick={sender ? () => {acceptRequest(sender)} : () => {sendRequest(receiver);setShowModal(false);}}
                    >
                        {sender ? "Accept" : "Send"}
                    </button>
                    {sender && <button
                        type="button"
                        className="w-full inline-flex justify-center rounded-md shadow-sm px-4 py-2 bg-red-600 font-medium text-white hover:bg-red-700 ml-3 w-auto text-sm"
                        onClick={() => setShowModal(false)}
                    >
                        Reject
                    </button>}
                </div>
            </div>
        </div>
    );
}

export default Modal;
