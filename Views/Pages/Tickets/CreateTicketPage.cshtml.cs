using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Business;
using Daos;
using Services.TicketService;

namespace Views.Pages
{
    public class CreateTicketPageModel(ITicketService ticketService) : PageModel
    {
        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var ticketTypes = await ticketService.GetAllTicketType();
            ViewData["TypeId"] = new SelectList(ticketTypes, "Id", "Type");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                string? logedInUser = HttpContext.Session.GetString("LogedInUser");
                User? user = logedInUser != null ? JsonUtil.ReadJsonItem<User>(logedInUser) : null;

                Ticket.OwnerId = loggedInUser.Id;
                Ticket.StatusId = 1;

                await ticketService.AddAsync(Ticket);

                return RedirectToPage("MyTicketsPage");
            }


            var ticketTypes = await ticketService.GetAllTicketType();
            ViewData["TypeId"] = new SelectList(ticketTypes, "Id", "Type");
            return Page();
        }
    }
}
