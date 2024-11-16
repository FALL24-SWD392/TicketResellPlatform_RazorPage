using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Business;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.TicketService;
using Utils;

namespace Views.Pages.Manager
{
    public class TicketsModel : PageModel
    {
        private readonly ITicketService _ticketService;
        private readonly INotyfService _notyfService;
        public TicketsModel(ITicketService ticketService, INotyfService notyfService)
        {
            _ticketService = ticketService;
            _notyfService = notyfService;
        }
        [BindProperty] public IList<Ticket> Tickets { get; set; } = [];
        public async Task<IActionResult> OnGetAsync()
        {
            Tickets = (await _ticketService.GetAllAsync());
            return Page();
        }
        public async Task<IActionResult> OnPostApprovedTicketAsync(int id)
        {
            string? logedInUser = HttpContext.Session.GetString("LogedInUser");
            User? manager = logedInUser != null ? JsonUtil.ReadJsonItem<User>(logedInUser) : null;
            var ticket = await _ticketService.GetAsync(id);
            if (manager == null)
            {
                _notyfService.Error("User not found");
            }
            else if (ticket == null)
            {
                _notyfService.Error("Ticket not found");
            }
            else
            {
                ticket.StatusId = 2;

                await _ticketService.UpdateAsync(ticket);
            }
            Tickets = (await _ticketService.GetAllAsync()).ToList();
            return Page();
        }
        public async Task<IActionResult> OnPostRejectedTicketAsync(int id)
        {
            string? logedInUser = HttpContext.Session.GetString("LogedInUser");
            User? manager = logedInUser != null ? JsonUtil.ReadJsonItem<User>(logedInUser) : null;
            var ticket = await _ticketService.GetAsync(id);
            if (manager == null)
            {
                _notyfService.Error("User not found");
            }
            else if (ticket == null)
            {
                _notyfService.Error("Ticket not found");
            }
            else
            {
                ticket.StatusId = 3;
                await _ticketService.UpdateAsync(ticket);
            }
            Tickets = (await _ticketService.GetAllAsync()).Where(t => t.StatusId == 1).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveTicketAsync(int id)
        {
            string? logedInUser = HttpContext.Session.GetString("LogedInUser");
            User? manager = logedInUser != null ? JsonUtil.ReadJsonItem<User>(logedInUser) : null;
            var ticket = await _ticketService.GetAsync(id);
            if (manager == null)
            {
                _notyfService.Error("User not found");
            }
            else if (ticket == null)
            {
                _notyfService.Error("Ticket not found");
            }
            else
            {
                ticket.StatusId = 6;
                await _ticketService.UpdateAsync(ticket);
            }
            Tickets = (await _ticketService.GetAllAsync()).Where(t => t.StatusId == 1).ToList();
            return Page();
        }
    }
}
