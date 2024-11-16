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
            var user = JsonConvert.DeserializeObject<User>(Context.GetHttpContext().Session.GetString("LogedInUser"));

            await base.OnConnectedAsync();
        }

        public async Task SendMessage(ChatMessage chatMessage, Chatbox chatbox)
        {
            var result = await _chatService.CreateChatMessage(chatMessage);

            await Clients.Client(chatbox.BuyerId.ToString()).SendAsync("ReceiveMessage", result);
            await Clients.Client(chatbox.SellerId.ToString()).SendAsync("ReceiveMessage", result);
        }
    }
}
