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
using Services.UserService;
using Services.ChatService;
using Services.OrderService;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Views.Pages.ReportPage
{
    public class DetailsModel : PageModel
    {
        private readonly IReportService _reportService;
        private readonly IOrderService _orderService;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        public DetailsModel(IReportService reportService, IOrderService orderService, IChatService chatService, IUserService userService)
        {
            _reportService = reportService;
            _chatService = chatService;
            _orderService = orderService;
            _userService = userService;
        }

        [BindProperty]
        public Report Report { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _reportService.GetAsync(id.Value);
            if (report == null)
            {
                return NotFound();
            }
            else
            {
                var order = await _orderService.GetAsync(report.OrderId);
                if (order == null)
                {
                    return NotFound();
                }
                var chat = await _chatService.GetAsync(order.ChatBoxId);
                if (chat == null)
                {
                    return NotFound();
                }
                var buyer = await _userService.GetAsync(chat.BuyerId);
                var seller = await _userService.GetAsync(chat.SellerId);
                if (seller != null && buyer != null)
                {
                    chat.Seller = seller;
                    chat.Buyer = buyer;
                    order.ChatBox = chat;
                    report.Order = order;
                }
                else
                {
                    return NotFound();
                }
                Report = report;
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string status)
        {
            var report = await _reportService.GetAsync(Report.Id);
            if (report == null)
            {
                return NotFound();
            }
            if (status == "Approved")
            {
                report.StatusId = 2;
            }
            else if (status == "Rejected")
            {
                report.StatusId = 3;
            }
            await _reportService.UpdateAsync(report);
            return RedirectToPage("./Index");
        }
    }
}
