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
    public class DetailsModel : PageModel
    {
        private readonly IMembershipService _membershipService;

        public DetailsModel(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        public Membership Membership { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var userJson = HttpContext.Session.GetString("LogedInUser");
            if (userJson == null)
            {
                return RedirectToPage("/Login");
            }
            else
            {
                var owner = JsonUtil.ReadJsonItem<User>(userJson);
                if (owner == null)
                {
                    return RedirectToPage("/Login");
                }
                else
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var membership = await _membershipService.GetAsync(id.Value);
                    if (membership == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        if (membership.OwnerId != owner.Id)
                            return RedirectToPage("Index");
                        else
                            Membership = membership;
                    }
                }
            }
            return Page();
        }
    }
}
