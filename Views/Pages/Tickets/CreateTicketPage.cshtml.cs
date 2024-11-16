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
using Utils;

namespace Views.Pages
{
    public class CreateTicketPageModel(ITicketService ticketService) : PageModel
    {
        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        [BindProperty]
        public IFormFile Image { get; set; }

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
                if(user != null)
                    Ticket.OwnerId = user.Id;
                Ticket.StatusId = 1;

                if (Image != null)
                {
                    var fileName = Path.GetFileName(Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(stream);
                    }

                    Ticket.Image = fileName;
                }

                await ticketService.AddAsync(Ticket);

                return RedirectToPage("MyTicketsPage");
            }


            var ticketTypes = await ticketService.GetAllTicketType();
            ViewData["TypeId"] = new SelectList(ticketTypes, "Id", "Type");
            return Page();
        }
    }
}
