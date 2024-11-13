using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.UserService;

namespace Views.Pages
{
    public class LoginModel(IUserService userService) : PageModel
    {
        private readonly IUserService userService = userService;

        [BindProperty] public string Email { get; set; } = null!;
        [BindProperty] public string Password { get; set; } = null!;
        public void OnGet()
        {
        }
    }
}
