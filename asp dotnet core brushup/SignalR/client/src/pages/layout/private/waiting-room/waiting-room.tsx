import { UseWaitingRoomStore } from "./store";


const WaitingRoom = () => {
    const { userName, setUserName, chatRoom, setChatRoom, joinRoom } = UseWaitingRoomStore();


    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!userName || !chatRoom) return alert("Both fields are required!");
        joinRoom({ userName, chatRoom });
    };


    return (
        <div className="flex items-center justify-center  bg-gray-100">
            <form
                onSubmit={handleSubmit}
                className="bg-white p-8 rounded-2xl shadow-md w-full max-w-sm"
            >
                <h2 className="text-2xl font-bold mb-6 text-center text-gray-800">
                    Join Chat Room
                </h2>

                <div className="mb-4">
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                        Username
                    </label>
                    <input
                        type="text"
                        value={userName}
                        onChange={(e) => setUserName(e.target.value)}
                        className="w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
                        placeholder="Enter your name"
                    />
                </div>

                <div className="mb-6">
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                        Chat Room
                    </label>
                    <input
                        type="text"
                        value={chatRoom}
                        onChange={(e) => setChatRoom(e.target.value)}
                        className="w-full px-4 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
                        placeholder="Enter chat room name"
                    />
                </div>

                <button
                    type="submit"
                    className="w-full bg-blue-600 text-white py-2 rounded-lg hover:bg-blue-700 transition"
                >
                    Join
                </button>
            </form>
        </div>
    );
};

export default WaitingRoom;
