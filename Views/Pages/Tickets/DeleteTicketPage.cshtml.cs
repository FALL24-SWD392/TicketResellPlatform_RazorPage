using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Business;
using Daos;
using Services.TicketService;

namespace Views.Pages.Tickets
{
    public class DeleteTicketPageModel : PageModel
    {
        private readonly ITicketService _ticketService;

        public DeleteTicketPageModel(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var ticket = await _ticketService.GetAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }
            else
            {
                Ticket = ticket;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Ticket = await _ticketService.GetAsync(id);
            Ticket.Status.Id = 6;
            await _ticketService.UpdateAsync(Ticket);
            return RedirectToPage("../MyTicketsPage");
        }
    }
}
