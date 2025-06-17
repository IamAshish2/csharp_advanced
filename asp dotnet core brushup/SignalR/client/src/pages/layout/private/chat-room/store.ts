import { create } from "zustand";
import { IGroupMessageData, IUseChatRoomStore } from "./interface";
import { useSignalRStore } from "@/global/components/signalR/store";

export const useChatRoomStore = create<IUseChatRoomStore>((set, get) => ({
    messages: [],

    setMessages: (message: IGroupMessageData) => {
        set({ messages: [...get().messages, message] });
    },
    startListeningToMessages: () => {
        const conn = useSignalRStore.getState().connection;

        if (conn) {
            conn.off("ReceiveMessage");
            conn.off("ReceiveSpecificMessage");

            conn.on("ReceiveMessage", (userName: string, message: string) => {
                console.log(userName, " sent a message: ", message);
            });

            conn.on("ReceiveSpecificMessage", (userName: string, message: string) => {
                get().setMessages({ userName, message });
            });
        }
    },

    sendMessagesToChatRoom: async (data) => {
        const conn = useSignalRStore.getState().connection;

        if (!data.message.trim()) return;

        if (conn) {
            try {
                await conn.invoke("SendMessageAsync", data.message);
            } catch (err) {
                console.error("Failed to send message:", err);
            }
        }
    },

    leaveRoom: () => {
        const conn = useSignalRStore.getState().connection;

        if (conn) {
            conn.off("ReceiveMessage");
            conn.off("ReceiveSpecificMessage");
            conn.stop();
        }

        set({
            messages: []
        })
    }
}))