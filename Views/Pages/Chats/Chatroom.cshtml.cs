using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repositories.ChatboxRepository;
using Services.ChatService;

namespace Views.Pages.Chats
{
    public class ChatroomModel : PageModel
    {
        private readonly IChatService _chatService;

        public ChatroomModel(IChatService chatService)
        {
            _chatService = chatService;
        }
        public IList<Chatbox> Chatboxes { get; set; } = default!;
        public Chatbox Chatbox { get; set; } = default!;

        public async void OnGet(int id)
        {
            if (await _chatService.GetAllAsync() != null)
            {
                Chatboxes = await _chatService.GetAllAsync();

                Chatbox = await _chatService.GetAsync(id);
            }
        }

        public async void OnPost()
        {
            string message = Request.Form["txtMessage"];
            ChatMessage chatMessage = new()
            {
                ChatBox = Chatbox,
                ChatBoxId = Chatbox.Id,
                Message = message,
                Sender = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("LogedInUser")),
                SenderId = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("LogedInUser")).Id,
            };
            SignalRHub signalRHub = new SignalRHub(_chatService);
            await signalRHub.SendMessage(chatMessage, Chatbox);
        }
    }
}
