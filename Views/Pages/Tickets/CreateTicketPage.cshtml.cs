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
        //public IActionResult OnGet()
        //{
        //    ViewData["TypeId"] = new SelectList(ticketService., "Id", "Type");
        //    return Page();
        //}

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await ticketService.AddAsync(Ticket);

            return RedirectToPage("MyTicketsPage");
        }
    }
}
