using Business;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Services.ChatService;
using Sprache;

namespace Views
{
    public class SignalRHub : Hub
    {
        private readonly IChatService _chatService;

        public SignalRHub(IChatService chatService)
        {
            _chatService = chatService;
        }
        public override async Task OnConnectedAsync()
        {

            await base.OnConnectedAsync();
        }

        public async Task SendMessage(ChatMessage chatMessage, Chatbox chatbox, User user)
        {
            var result = await _chatService.CreateChatMessage(chatMessage);
            if (chatbox.BuyerId == user.Id)
            {
                await Clients.Client(chatbox.BuyerId.ToString()).SendAsync("MessageSended", result);
                await Clients.Client(chatbox.SellerId.ToString()).SendAsync("ReceiveMessage", result);
            }
            else
            {
                await Clients.Client(chatbox.BuyerId.ToString()).SendAsync("ReceiveMessage", result);
                await Clients.Client(chatbox.SellerId.ToString()).SendAsync("MessageSended", result);
            }
            
        }
    }
}
