using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.TicketService;

namespace Views.Pages.Tickets
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();

        private readonly ITicketService _ticketService;

        public IndexModel(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task OnGetAsync()
        {
            Tickets = await _ticketService.GetAllAsync();
        }
    }
}
