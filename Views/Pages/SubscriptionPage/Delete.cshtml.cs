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
    public class DeleteModel : PageModel
    {
        private readonly ISubscriptionService _subscriptionService;

        public DeleteModel(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [BindProperty]
        public Subcription Subcription { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcription = await _subscriptionService.GetAsync(id.Value);

            if (subcription == null)
            {
                return NotFound();
            }
            else
            {
                Subcription = subcription;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcription = await _subscriptionService.GetAsync(id.Value);
            if (subcription != null)
            {
                Subcription = subcription;
                await _subscriptionService.DeleteAsync(Subcription.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
