using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Business;
using Daos;
using Services.SubscriptionService;

namespace Views.Pages.SubscriptionPage
{
    public class EditModel : PageModel
    {
        private readonly ISubscriptionService _subscriptionService;

        public EditModel(ISubscriptionService subscriptionService)
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
            Subcription = subcription;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _subscriptionService.UpdateAsync(Subcription);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubcriptionExists(Subcription.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SubcriptionExists(int id)
        {
            return _subscriptionService.GetAsync(id) != null;
        }
    }
}
