import { create } from "zustand";
import type { ISignalRStore } from "./interface";
import { HubConnectionBuilder, LogLevel, type HubConnection } from "@microsoft/signalr";

export const useSignalRStore = create<ISignalRStore>((set,get) => ({
  connection: undefined,
  setConnection: (conn:HubConnection) => {
    set({
        connection: conn
    })
  }, 
  initiateConnection: async () => {
    const conn = new HubConnectionBuilder()
                .withUrl("https://localhost:7246/messages")
                .configureLogging(LogLevel.Information)
                .build();
    
    await conn.start();

    if(conn){
        get().setConnection(conn);
        return conn;
    }
    return null;
  }
}));



  // async function joinRoom(userName: string, chatRoom: string) {
  //   try {
  //     // initiate a connection
  //     const conn = new HubConnectionBuilder()
  //       .withUrl("https://localhost:7246/messages")
  //       .configureLogging(LogLevel.Information)
  //       .build();

  //     // set up handlers
  //     // the ReceiveMessage handler
  //     conn.on("ReceiveMessage", (userName: string, message: string) => {
  //       console.log(userName, " sent a message: ", message);
  //     });

  //     // the ReceiveSpecificMessage handler
  //     conn.on("ReceiveSpecificMessage", (userName: string, message: string) => {
  //       setMessages(prev => [...prev, { userName, message }])
  //     });

  //     await conn.start();
  //     await conn.invoke("JoinSpecificChatRoom", { userName, chatRoom });

  //     setConnection(conn);
  //   } catch (err: any) {
  //     console.log(err.message);
  //     console.log(err);
  //   }
  // }