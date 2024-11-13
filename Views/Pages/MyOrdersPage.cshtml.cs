using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Business;
using Daos;
using Services.OrderService;

namespace Views.Pages
{
    public class MyOrdersPageModel(IOrderService orderService) : PageModel
    {
        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Order = await orderService.GetAllAsync();
        }
    }
}
