using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Business;
using Daos;
using Services.MembershipService;
using Services.SubscriptionService;
using Utils;

namespace Views.Pages.MembershipPage
{
    public class IndexModel : PageModel
    {
        private readonly IMembershipService _membershipService;

        public IndexModel(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        public IList<Membership> Membership { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var userJson = HttpContext.Session.GetString("LogedInUser");
            if (userJson == null)
            {
                return RedirectToPage("/Login");
            }
            else
            {
                var owner = JsonUtil.ReadJsonItem<User>(userJson);
                var memberships = (await _membershipService.GetAllAsync()).Where(m => m.OwnerId == owner.Id).ToList();
                Membership = memberships.OrderByDescending(m => m.StartDate).ToList();
            }
            return Page();
        }
    }
}
