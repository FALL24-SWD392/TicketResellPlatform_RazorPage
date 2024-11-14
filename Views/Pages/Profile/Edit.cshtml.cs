using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Views.Pages.Profile
{
    public class EditModel : PageModel
    {
        [BindProperty] public User Profile { get; set; } = null!;
        public async Task<IActionResult> OnGetAsync()
        {
            Profile = new User()
            {
                Email = "user@example.com",
                Username = "example user",
                Avatar = "/images/static/user-avatar.png",
                Rating = 4.5,
                Reputation = 100,
            };
            return Page();
        }
    }
}
