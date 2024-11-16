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
            Subcription = (IList<Subcription>)(await _subscriptionService.GetAllAsync()).Where(s => !s.Description.Contains("Decrepated")).ToList();
        }
    }
}
