using Microsoft.AspNetCore.SignalR;
using Showcase.Web.Models;

namespace Showcase.Web.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(ChatMessage chatMessage)
        {
            // Broadcast the message to all clients
            await Clients.All.SendAsync("ReceiveMessage", chatMessage);
        }
    }
}
