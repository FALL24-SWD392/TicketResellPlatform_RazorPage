using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Packaging;
using Repositories.ChatboxRepository;
using Services.ChatService;
using Services.OrderService;
using Utils;

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
        public IList<ChatMessage> Messages { get; set; }
        public Chatbox Chatbox { get; set; } = default!;

        public async void OnGet(int id)
        {
            User user = JsonUtil.ReadJsonItem<User>(HttpContext.Session.GetString("LogedInUser"));
            if (await _chatService.GetChatboxOfUser(user.Id) != null)
            {
                Chatboxes = await _chatService.GetChatboxOfUser(user.Id);

                try
                {
                    foreach (var chatbox in Chatboxes)
                    {
                        Messages.AddRange(chatbox.ChatMessages);
                    }
                }
                catch
                {

                }

                Chatbox = await _chatService.GetAsync(id);
            }
        }

        public async void OnPost(int id)
        {
            User user = JsonUtil.ReadJsonItem<User>(HttpContext.Session.GetString("LogedInUser"));
            Chatbox = await _chatService.GetAsync(id);
            Chatboxes = await _chatService.GetChatboxOfUser(user.Id);

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
                    Sender = user,
                    SenderId = user.Id,
                };
                SignalRHub signalRHub = new SignalRHub(_chatService);
                await signalRHub.SendMessage(chatMessage, Chatbox, user);
            }
        }
    }
}
