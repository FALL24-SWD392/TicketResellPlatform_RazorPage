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
using Utils;
using System.Net.Sockets;

namespace Views.Pages.Tickets
{
    public class EditTicketPageModel : PageModel
    {
        private readonly ITicketService _ticketService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EditTicketPageModel(ITicketService ticketService, IWebHostEnvironment webHostEnvironment)
        {
            this._ticketService = ticketService;
            this._webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Ticket Ticket { get; set; } = default!;

        [BindProperty]
        public IFormFile Image { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var ticket = await _ticketService.GetAsync(id);
            var ticketTypes = await _ticketService.GetAllTicketType();
            ViewData["TypeId"] = new SelectList(ticketTypes, "Id", "Type");
            Ticket = ticket;
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
                Ticket.CreateAt = Ticket.CreateAt;

                var uploadedImage = Request.Form.Files.FirstOrDefault();
                if (uploadedImage != null)
                {
                    Ticket.Image = ProcessUploadedFile(uploadedImage);
                }
                else
                {
                    Ticket.Image = Ticket.Image;
                }

                await _ticketService.UpdateAsync(Ticket);
                return RedirectToPage("../MyTicketsPage");
            }

            var ticketTypes = await _ticketService.GetAllTicketType();
            ViewData["TypeId"] = new SelectList(ticketTypes, "Id", "Type");
            return Page();
        }

        private bool TicketExists(int id)
        {
            return _ticketService.GetAsync(id) != null;
        }

        private string ProcessUploadedFile(IFormFile file)
        {
            string uniqueFileName = null;
            if (file != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
