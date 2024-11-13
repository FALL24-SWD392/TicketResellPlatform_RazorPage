using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Business;
using Daos;
using Services.FeedbackService;
using Services.OrderService;
using Services.ChatService;
using Services.UserService;

namespace Views.Pages.FeedbackPage
{
    public class CreateModel : PageModel
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IOrderService _orderService;
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        public CreateModel(IFeedbackService feedbackService, IOrderService orderService, IChatService chatService, IUserService userService)
        {
            _feedbackService = feedbackService;
            _orderService = orderService;
            _chatService = chatService;
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["OrderId"] = new SelectList(await _orderService.GetAllAsync(), "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Feedback Feedback { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Feedback.Order");
            if (!ModelState.IsValid)
            {
                ViewData["OrderId"] = new SelectList(await _orderService.GetAllAsync(), "Id", "Id");
                return Page();
            }

            var order = await _orderService.GetAsync(Feedback.OrderId);
            if (order == null)
            {
                ViewData["OrderId"] = new SelectList(await _orderService.GetAllAsync(), "Id", "Id");
                return Page();
            }

            var chatBox = await _chatService.GetAsync(order.ChatBoxId);
            if (chatBox == null)
            {
                ViewData["OrderId"] = new SelectList(await _orderService.GetAllAsync(), "Id", "Id");
                return Page();
            }

            var seller = await _userService.GetAsync(chatBox.SellerId);
            if (seller == null)
            {
                ViewData["OrderId"] = new SelectList(await _orderService.GetAllAsync(), "Id", "Id");
                return Page();
            }

            await _feedbackService.AddAsync(Feedback);

            //Get all chatboxes of the seller
            var chatBoxes = (await _chatService.GetAllAsync()).Where(c => c.SellerId == seller.Id && c.StatusId == 2);
            //Get all orders of the chatboxes
            var orders = (await _orderService.GetAllAsync()).Where(o => chatBoxes.Any(c => o.ChatBoxId == c.Id));
            //Get all feedbacks of the orders
            var feedbacks = (await _feedbackService.GetAllAsync()).Where(f => orders.Any(o => f.OrderId == o.Id));
            //Calculate the average rating
            var rating = feedbacks.Average(f => f.Rating);
            seller.Rating = rating;
            await _userService.UpdateAsync(seller);


            return RedirectToPage("./Index");
        }
    }
}
