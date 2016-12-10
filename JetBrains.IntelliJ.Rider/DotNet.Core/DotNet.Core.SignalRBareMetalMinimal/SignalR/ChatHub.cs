
namespace DotNet.Core.SignalRBareMetalMinimal.SignalR
{
    public partial class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public static System.Collections.Generic.List<string> ConnectedUsers;

        public void Send(string originatorUser, string message)
        {
            Clients.All.messageReceived(originatorUser, message);
        }

        public void Connect(string newUser)
        {
            if (ConnectedUsers == null)
                ConnectedUsers = new System.Collections.Generic.List<string>();

            ConnectedUsers.Add(newUser);
            Clients.Caller.getConnectedUsers(ConnectedUsers);
            Clients.Others.newUserAdded(newUser);
        }
    }
}