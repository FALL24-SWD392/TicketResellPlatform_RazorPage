using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.MembershipService;
using Services.PayOSService;
using Services.SubscriptionService;
using Utils;

namespace Views.Pages.Payment
{
    public class IndexModel(IPayOSService payOSService, ISubscriptionService subscriptionService, IMembershipService membershipService) : PageModel
    {
        [BindProperty] public Membership Membership { get; set; } = null!;
        [BindProperty] public Subcription Subcription { get; set; } = null!;
        public async Task<IActionResult> OnGetAsync(int subscriptionId)
        {
            Subcription = await subscriptionService.GetAsync(subscriptionId);
            Membership = await payOSService.GetPaymentLink(subscriptionId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Membership membership, int subscriptionId)
        {
            var jsonUser = HttpContext.Session.GetString("LogedInUser");
            User user = JsonUtil.ReadJsonItem<User>(jsonUser);
            membership.OwnerId = user.Id;
            membership.SupscriptionId = subscriptionId;
            membership.StatusId = 4;
            membership = await membershipService.AddAsync(membership);
            return RedirectToPage("/Profile/Index");
        }
    }
}
