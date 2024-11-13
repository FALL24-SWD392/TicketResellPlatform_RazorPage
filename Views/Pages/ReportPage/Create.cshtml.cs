using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Business;
using Daos;
using Services.ReportService;
using Services.ChatService;
using Services.OrderService;
using Services.UserService;

namespace Views.Pages.ReportPage
{
    public class CreateModel : PageModel
    {
        private readonly IReportService _reportService;

        public CreateModel(IReportService reportService)
        {
            _reportService = reportService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Report Report { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Report.Order");
            ModelState.Remove("Report.Sender");
            ModelState.Remove("Report.Status");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Report.StatusId = 1;

            await _reportService.AddAsync(Report);

            return RedirectToPage("./Index");
        }
    }
}
