//using Microsoft.AspNetCore.SignalR;
//using Signal_R.Data;
//using Signal_R.Models;

//namespace Signal_R.Hubs
//{
//    [ObsoleteAttribute("The message Hub is deperacted and utilized for testing signalR only.")]
//    public class MessageHub : Hub
//    {
//        private readonly SharedDb _sharedDb;
//        public MessageHub(SharedDb sharedDb) => _sharedDb = sharedDb;
        
//        public async Task JoinChatRoomAsync(UserConnection conn)
//        {
//            await Clients.All
//                .SendAsync("ReceiveMessage","admin",$"The user {conn.UserName} has joined the chatRoom.");
//        }

//        public Task SendMessageToAllAsync(string message)
//        {
//            return Clients.All.SendAsync("ReceiveMessage", message);
//        }

//        public async Task JoinSpecificChatRoom(UserConnection conn)
//        {
//            // add the user's connectionId to the chatRoom
//            await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);

//            // store the connection in db
//            _sharedDb.Connections[Context.ConnectionId] = conn;

//            // specify the send back message to the group.
//            await Clients.Group(conn.ChatRoom).SendAsync("ReceiveMessage", "admin", $"The user {conn.UserName} has joined the {conn.ChatRoom}.");
//        }

//        public async Task SendMessageAsync(string message)
//        {
//            if (_sharedDb.Connections.TryGetValue(Context.ConnectionId, out UserConnection conn))
//            {
//                await Clients.Group(conn.ChatRoom)
//                       .SendAsync("ReceiveSpecificMessage", conn.UserName, message);
//            }
//        }
//    }
//}
