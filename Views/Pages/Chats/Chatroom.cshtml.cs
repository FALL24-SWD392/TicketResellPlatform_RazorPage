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
        [BindProperty] public IList<Chatbox> Chatboxes { get; set; } = default!;
        [BindProperty] public IList<ChatMessage> Messages { get; set; }
        [BindProperty] public Chatbox Chatbox { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            User user = JsonUtil.ReadJsonItem<User>(HttpContext.Session.GetString("LogedInUser"));
            Chatboxes = await _chatService.GetChatboxOfUser(user.Id);
            Chatbox = await _chatService.GetAsync(id);
            return Page();
        }

        public async Task<IActionResult> OnPostCreateOrderAsync(int id)
        {
            User user = JsonUtil.ReadJsonItem<User>(HttpContext.Session.GetString("LogedInUser"));
            Chatbox = await _chatService.GetAsync(id);
            Chatboxes = await _chatService.GetChatboxOfUser(user.Id);
            Chatbox.StatusId = 2;
            Order order = new()
            {
                ChatBoxId = Chatbox.Id,
                CreateAt = DateTime.Now,
                Quantity = 1,
            };
            await _chatService.UpdateAsync(Chatbox);
            await _orderService.AddAsync(order);
            return Redirect("/Orders");
        }

        public async Task<IActionResult> OnPostCancelOrderAsync(int id)
        {
            User user = JsonUtil.ReadJsonItem<User>(HttpContext.Session.GetString("LogedInUser"));
            Chatbox = await _chatService.GetAsync(id);
            Chatboxes = await _chatService.GetChatboxOfUser(user.Id);
            Chatbox.StatusId = 3;
            await _chatService.UpdateAsync(Chatbox);
            return Redirect("/Chats");
        }

        public async void OnPost(int id)
        {
            User user = JsonUtil.ReadJsonItem<User>(HttpContext.Session.GetString("LogedInUser"));
            Chatbox = await _chatService.GetAsync(id);
            Chatboxes = await _chatService.GetChatboxOfUser(user.Id);

            
                string message = Request.Form["txtMessage"];
                ChatMessage chatMessage = new()
                {
                    ChatBoxId = Chatbox.Id,
                    Message = message,
                    SenderId = user.Id,
                };
                SignalRHub signalRHub = new SignalRHub(_chatService);
                await signalRHub.SendMessage(chatMessage, Chatbox, user);
            
        }
    }
}
