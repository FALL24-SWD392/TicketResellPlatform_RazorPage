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
                Ticket = new Ticket()
                {
                    Id = 0,
                    Title = "Not Found",
                    Description = "The ticket is not found",
                    Image = "https://placehold.co/400",
                    OwnerId = 0,
                    TypeId = 0,
                    StatusId = 0,
                    ExpiryDate = DateTime.Now,
                    Price = 0,
                    Quantity = 0,
                    CreateAt = DateTime.Now,
                    Type = new TicketType()
                    {
                        Id = 0,
                        Type = "Not Found",
                    },
                    Owner = new User()
                    {
                        Id = 0,
                        Username = "Not Found",
                        Rating = 5,
                        Reputation = 100,
                    }
                };
                return Page();
            }
            var ticketTypes = await ticketService.GetAllTicketType();
            ViewData["TypeId"] = new SelectList(ticketTypes, "Id", "Type");
            Ticket = ticket;
            return Page();
        }

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
