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

namespace Views.Pages
{
    public class MyTicketsPageModel : PageModel
    {
        private readonly ITicketService _ticketService;

        public MyTicketsPageModel(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public IList<Ticket> Ticket { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Ticket = await _ticketService.GetAllAsync();
        }
    }
}
