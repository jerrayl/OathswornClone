import { useEffect, useState } from "react";
import title from "./assets/title.png";
import Modal from "./Modal";

function Title({ connected, email, setEmail, loggedIn, login, sendRequest, acceptRequest, requests }) {
    const [showModal, setShowModal] = useState(false);
    const [sender, setSender] = useState("");

    useEffect(() => {
        if (requests.length > 0){
            setSender(requests[0].sender);
            setShowModal(true);
        }
    }, [requests])
        
    return (
        <>
            {showModal && <Modal sender={sender} setShowModal={setShowModal} sendRequest={sendRequest} acceptRequest={acceptRequest}/>}
            <div className="flex left-3 top-3 fixed">
                {connected ?
                    <h2 className="font-bold">Connected</h2> :
                    <h2 className="font-bold">Connecting...</h2>
                }
            </div>
            <div className="flex right-3 top-3 fixed">
                {loggedIn ?
                    <>
                        <h2 className="font-bold">{email}</h2>
                    </> :
                    <>
                        <input className="bg-stone-600 p-2 rounded-tl-md rounded-bl-md outline-none text-stone-200" value={email} onChange={e => setEmail(e.target.value)} />
                        <button className="bg-stone-700 hover:bg-stone-600 py-2 w-16 rounded-tr-md rounded-br-md text-white" onClick={() => login(email)}>Login</button>
                    </>
                }
            </div>
            <div className="flex flex-col items-center font-serif">
                <img src={title} className="w-1/2" alt="title" />
                {loggedIn && <button className="bg-stone-700 hover:bg-stone-600 text-white text-4xl font-bold py-10 w-96 rounded-xl"
                    onClick={() => setShowModal(true)}>
                    Start
                </button>}
                <button className="mt-4 bg-stone-700 hover:bg-stone-600 text-white text-4xl font-bold py-10 w-96 rounded-xl">
                    Tutorial
                </button>
                <button className="mt-4 bg-stone-700 hover:bg-stone-600 text-white text-4xl font-bold py-10 w-96 rounded-xl">
                    Credits
                </button>
            </div>
        </>
    );
}

export default Title;
