using Microsoft.AspNetCore.SignalR;
using Showcase.Web.Models;

namespace Showcase.Web.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(ChatMessage chatMessage)
        {
            await Clients.All.SendAsync("ReceiveMessage", chatMessage);
        }

        public async Task DeleteMessage(string chatMessageId)
        {
            await Clients.All.SendAsync("MessageDeleted", chatMessageId);
        }
    }
}
