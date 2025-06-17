export interface IGroupData {
  userName: string;
  chatRoom: string;
}

export interface IGroupMessageData{
    userName: string;
    message: string;
}

export interface IWaitingRoomStore{
    userName: string;
    chatRoom: string;
    setUserName: (userName: string) => void;
    setChatRoom: (chatRoom: string) => void;

    // join a specific room
    joinRoom: (data:IGroupData) => void;
}