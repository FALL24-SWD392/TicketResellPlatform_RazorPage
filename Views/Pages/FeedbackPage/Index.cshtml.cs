using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Business;
using Daos;
using Services.FeedbackService;
using Services.OrderService;
using Services.ChatService;
using Services.UserService;

namespace Views.Pages.FeedbackPage
{
    public class IndexModel : PageModel
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IOrderService _orderService;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        public IndexModel(IFeedbackService feedbackService, IOrderService orderService, IChatService chatService, IUserService userService)
        {
            _feedbackService = feedbackService;
            _orderService = orderService;
            _chatService = chatService;
            _userService = userService;
        }

        public IList<Feedback> Feedback { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var feedbacks = await _feedbackService.GetAllAsync();
            foreach (var item in feedbacks)
            {
                var order = await _orderService.GetAsync(item.OrderId);
                if (order != null)
                {
                    var chatbox = await _chatService.GetAsync(order.ChatBoxId);
                    if (chatbox != null)
                    {
                        var seller = await _userService.GetAsync(chatbox.SellerId);
                        var buyer = await _userService.GetAsync(chatbox.BuyerId);
                        if (seller != null && buyer != null)
                        {
                            chatbox.Seller = seller;
                            chatbox.Buyer = buyer;
                            order.ChatBox = chatbox;
                            item.Order = order;
                        }
                    }
                }
            }
            Feedback = feedbacks;
        }
    }
}
