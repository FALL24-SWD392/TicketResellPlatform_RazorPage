using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Business;
using Daos;
using Services.ReportService;
using Services.OrderService;
using Services.ChatService;
using Services.UserService;

namespace Views.Pages.ReportPage
{
    public class IndexModel : PageModel
    {
        private readonly IReportService _reportService;
        private readonly IOrderService _orderService;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        public IndexModel(IReportService reportService, IOrderService orderService, IChatService chatService, IUserService userService)
        {
            _reportService = reportService;
            _orderService = orderService;
            _chatService = chatService;
            _userService = userService;
        }

        public IList<Report> Report { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var report = await _reportService.GetAllAsync();
            foreach (var item in report)
            {
                var order = await _orderService.GetAsync(item.OrderId);
                if (order != null)
                {
                    var chat = await _chatService.GetAsync(order.ChatBoxId);
                    if (chat != null)
                    {
                        var buyer = await _userService.GetAsync(chat.BuyerId);
                        var seller = await _userService.GetAsync(chat.SellerId);
                        if (seller != null && buyer != null)
                        {
                            chat.Seller = seller;
                            chat.Buyer = buyer;
                            order.ChatBox = chat;
                            item.Order = order;
                        }
                    }
                }
            }
            Report = report;
        }
    }
}
