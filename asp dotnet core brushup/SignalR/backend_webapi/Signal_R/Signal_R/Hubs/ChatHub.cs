using Microsoft.AspNetCore.SignalR;

namespace Signal_R.Hubs
{
    public class ChatHub : Hub
    {
        //public async Task JoinGroup(int groupId)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, groupId.ToString());

        //    // specify the send back message to the group.
        //    await Clients.Group(conn.ChatRoom).SendAsync("ReceiveMessage", "admin", 
        //        $"The user {conn.UserName} has joined the {conn.ChatRoom}.");
        //}
    }
}
