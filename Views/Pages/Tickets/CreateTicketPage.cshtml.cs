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

namespace Views.Pages.Tickets
{
    public class CreateTicketPageModel : PageModel
    {
        private readonly ITicketService _ticketService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CreateTicketPageModel(ITicketService ticketService, IWebHostEnvironment webHostEnvironment)
        {
            this._ticketService = ticketService;
            this._webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        [BindProperty]
        public IFormFile Image { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var ticketTypes = await _ticketService.GetAllTicketType();
            ViewData["TypeId"] = new SelectList(ticketTypes, "Id", "Type");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                string? logedInUser = HttpContext.Session.GetString("LogedInUser");
                User? user = logedInUser != null ? JsonUtil.ReadJsonItem<User>(logedInUser) : null;
                if (user != null)
                    Ticket.OwnerId = user.Id;
                Ticket.StatusId = 1;

                if(Image != null)
                {
                    Ticket.Image = ProcessUploadedFile();
                }

                await _ticketService.AddAsync(Ticket);

                return RedirectToPage("../MyTicketsPage");
            }

            var ticketTypes = await _ticketService.GetAllTicketType();
            ViewData["TypeId"] = new SelectList(ticketTypes, "Id", "Type");
            return Page();
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if(Image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
