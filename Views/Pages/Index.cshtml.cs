using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.TicketService;

namespace Views.Pages
{
    public class IndexModel(ITicketService ticketService) : PageModel
    {
        [BindProperty] public IList<Ticket> Tickets { get; set; } = new List<Ticket>();

        public async Task<IActionResult> OnGetAsync()
        {
            Tickets = await ticketService.GetAllAsync();
            return Page();
        }
    }
}