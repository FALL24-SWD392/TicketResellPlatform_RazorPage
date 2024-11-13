using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Business;
using Daos;
using Services.SubscriptionService;

namespace Views.Pages.SubscriptionPage
{
    public class IndexModel : PageModel
    {
        private readonly ISubscriptionService _subscriptionService;

        public IndexModel(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        public IList<Subcription> Subcription { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // Subcription = (IList<Subcription>)(await _subscriptionService.GetAllAsync()).Where(s => !s.Description.Contains("Decrepated")).ToList();
            Subcription = [
                new Subcription { Id = 1, Description = "Description 1", Price = 10000 },
                new Subcription { Id = 2, Description = "Description 2", Price = 20000 },
                new Subcription { Id = 3, Description = "Description 3", Price = 30000 },
                new Subcription { Id = 4, Description = "Description 4", Price = 40000 }
            ];
        }
    }
}
