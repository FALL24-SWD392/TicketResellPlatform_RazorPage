using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.PayOSService;
using Services.SubscriptionService;

namespace Views.Pages.Payment
{
    public class IndexModel(IPayOSService payOSService, ISubscriptionService subscriptionService) : PageModel
    {
        [BindProperty] public Membership Membership { get; set; } = null!;
        [BindProperty] public Subcription Subcription { get; set; } = null!;
        public async Task<IActionResult> OnGetAsync(int subscriptionId)
        {
            Subcription = await subscriptionService.GetAsync(subscriptionId);
            Membership = await payOSService.GetPaymentLink(subscriptionId);
            return Page();
        }
    }
}
