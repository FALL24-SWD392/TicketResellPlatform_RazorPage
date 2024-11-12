using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Business;
using Daos;
using Services.SubscriptionService;

namespace Views.Pages.SubscriptionPage
{
    public class CreateModel : PageModel
    {
        private readonly ISubscriptionService _subscriptionService;

        public CreateModel(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Subcription Subcription { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _subscriptionService.AddAsync(Subcription);

            return RedirectToPage("./Index");
        }
    }
}
