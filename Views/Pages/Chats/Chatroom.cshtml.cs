using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repositories.ChatboxRepository;
using Services.ChatService;
using Services.OrderService;

namespace Views.Pages.Chats
{
    public class ChatroomModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IChatService _chatService;

        public ChatroomModel(IChatService chatService, IOrderService orderService)
        {
            _orderService = orderService;
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
            if (!string.IsNullOrEmpty(Request.Form["btnOrderTicket"]))
            {
                Chatbox.StatusId = 2;
                Order order = new()
                {
                    ChatBox = Chatbox,
                    ChatBoxId = Chatbox.Id,
                    CreateAt = DateTime.Now,
                    Quantity = 1,
                };
                await _chatService.UpdateAsync(Chatbox);
                await _orderService.AddAsync(order);
            }
            else if (!string.IsNullOrEmpty(Request.Form["btnCancelTicket"]))
            {
                Chatbox.StatusId = 3;
                Order order = new()
                {
                    ChatBox = Chatbox,
                    ChatBoxId = Chatbox.Id,
                    CreateAt = DateTime.Now,
                    Quantity = 1,
                };
                await _chatService.UpdateAsync(Chatbox);
                await _orderService.AddAsync(order);
            }
            else
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
}
