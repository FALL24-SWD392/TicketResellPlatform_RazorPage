using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.PayOSService;

namespace Views.Pages.Payment
{
    public class IndexModel(IPayOSService payOSService) : PageModel
    {
        [BindProperty] public Membership Membership { get; set; } = null!;
        [BindProperty] public Subcription Subcription { get; set; } = null!;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Subcription = new()
            {
                Description = "Monthly Subscription",
                Price = 100000,
                SaleLimit = 10,
                Name = "Monthly Subscription",
            };
            Membership = await payOSService.GetPaymentLink(id);
            return Page();
        }
    }
}
