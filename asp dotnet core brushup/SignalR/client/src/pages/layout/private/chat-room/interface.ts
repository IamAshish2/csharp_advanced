
export interface IUseChatRoomStore {
    // message: string;
    // sendMessage: (message: string) => void;
        messages: IGroupMessageData[],
        setMessages: (message:IGroupMessageData) => void;
         sendMessagesToChatRoom: (data: IGroupMessageData) => void;
            startListeningToMessages: () => void;
            leaveRoom : () => void;
}

export interface IGroupMessageData{
    userName: string;
    message: string;
}