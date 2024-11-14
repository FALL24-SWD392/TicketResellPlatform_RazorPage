using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Views.Pages.Profile
{
    public class ChangePasswordModel : PageModel
    {
        [BindProperty] public User Profile { get; set; } = null!;
        [BindProperty] public string OldPassword { get; set; } = string.Empty;
        [BindProperty] public string NewPassword { get; set; } = string.Empty;
        [BindProperty] public string ConfirmPassword { get; set; } = string.Empty;
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
