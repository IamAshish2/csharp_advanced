import { useState } from "react";
import { UseWaitingRoomStore } from "../waiting-room/store";
import { useChatRoomStore } from "./store";



const ChatRoom = () => {
    const [message, setMessage] = useState("");
    const { sendMessagesToChatRoom, messages } = useChatRoomStore();

    const { userName } = UseWaitingRoomStore();

    async function sendMessage() {
        if (!message.trim()) return;
        try {
            sendMessagesToChatRoom({ userName, message });
            setMessage("");
        } catch (err) {
            console.error("Failed to send message:", err);
        }
    }

    function handleKeyPress(e: React.KeyboardEvent<HTMLInputElement>) {
        if (e.key === "Enter") {
            sendMessage();
        }
    }

    return (
        <div className="bg-white rounded-xl shadow-lg p-4 max-w-md w-full mt-6 border border-gray-200 flex flex-col">
            <h2 className="text-xl font-semibold mb-3 text-center text-indigo-600">Chat Room</h2>

            {/* Message list */}
            <div className="overflow-y-auto flex-1 mb-4 pr-1 max-h-80">
                <ul className="space-y-3">
                    {messages.length === 0 ? (
                        <li className="text-center text-gray-400 italic">No messages yet...</li>
                    ) : (
                        messages.map((msg, idx) => (
                            <li key={idx} className="bg-indigo-50 p-3 rounded-lg shadow-sm">
                                <p className="text-sm text-gray-600 mb-1">
                                    <strong className="text-indigo-700">{msg.userName}</strong> said:
                                </p>
                                <p className="text-gray-800">{msg.message}</p>
                            </li>
                        ))
                    )}
                </ul>
            </div>

            {/* Message input */}
            <div className="flex items-center space-x-2 mt-auto">
                <input
                    type="text"
                    className="flex-1 border rounded-lg px-3 py-2 focus:outline-none focus:ring-2 focus:ring-indigo-400"
                    placeholder="Type a message..."
                    value={message}
                    onChange={(e) => setMessage(e.target.value)}
                    onKeyDown={handleKeyPress}
                />
                <button
                    className="bg-indigo-500 text-white px-4 py-2 rounded-lg hover:bg-indigo-600 transition"
                    onClick={sendMessage}
                >
                    Send
                </button>
            </div>
        </div>
    );
};

export default ChatRoom;
