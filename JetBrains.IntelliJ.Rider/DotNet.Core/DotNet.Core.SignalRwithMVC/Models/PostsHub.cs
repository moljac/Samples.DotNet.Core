namespace DotNet.Core.SignalR.Models
{
    /// <summary>
    /// hub as a proxy: clients will not call hub methods directly.
    /// SignalR will be used to provide server updates - that is the server notifies
    /// all clients that something happened, so the clients must first connect to a hub.
    /// </summary>
    public partial class PostsHub : Microsoft.AspNetCore.SignalR.Hub
    {

    }
}