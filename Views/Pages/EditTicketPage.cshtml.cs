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
using Services.TicketService;

namespace Views.Pages
{
    public class EditTicketPageModel(ITicketService ticketService) : PageModel
    {
        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var ticket = await ticketService.GetAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            Ticket = ticket;
           //ViewData["TypeId"] = new SelectList(_context.TicketTypes, "Id", "Type");
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
            await ticketService.UpdateAsync(Ticket);

            return RedirectToPage("MyTicketsPage");
        }

        private bool TicketExists(int id)
        {
            return ticketService.GetAsync(id) != null;
        }
    }
}
