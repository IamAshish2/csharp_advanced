import { create } from "zustand";
import { IGroupData, IWaitingRoomStore } from "./interface";
import { useSignalRStore } from "@/global/components/signalR/store";
import { useChatRoomStore } from "../chat-room/store";
import { useGlobalStore } from "@/global/store";

export const UseWaitingRoomStore = create<IWaitingRoomStore>((set, get) => ({
    userName: "",
    chatRoom: "",

    setUserName: (userName: string) => set({ userName }),
    setChatRoom: (chatRoom: string) => set({ chatRoom }),

    joinRoom: async (data: IGroupData) => {
        try {
            const conn = await useSignalRStore.getState().initiateConnection();

            if (conn) {
                await conn.invoke("JoinSpecificChatRoom", data);
                // actually use this when login to set the details
                useGlobalStore.getState().setAppUser({
                    id: useGlobalStore.getState().appUser.id,
                    userName: data.userName,
                    email: useGlobalStore.getState().appUser.email,
                    roles: [],
                    isNew: useGlobalStore.getState().appUser.isNew,
                })
                useChatRoomStore.getState().startListeningToMessages();
            }
        } catch (err: any) {
            console.error("Join room error:", err);
        }
    }
}));
