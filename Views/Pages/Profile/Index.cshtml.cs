using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.UserService;
using Utils;

namespace Views.Pages.Profile
{
    public class IndexModel(IUserService userService) : PageModel
    {
        [BindProperty] public User Profile { get; set; } = null!;
        public async Task<IActionResult> OnGetAsync()
        {
            string? logedInUser = HttpContext.Session.GetString("LogedInUser");
            User? user = logedInUser != null ? JsonUtil.ReadJsonItem<User>(logedInUser) : null;
            Profile = await userService.GetAsync(user.Id);
            return Page();
            
        }
    }
}
