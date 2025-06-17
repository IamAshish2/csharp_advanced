import type { HubConnection } from "@microsoft/signalr";

export interface ISignalRStore{
  connection: HubConnection | undefined,
  setConnection: (conn:HubConnection) => void;
  // start a connection
  initiateConnection: () => Promise<HubConnection | null>
}