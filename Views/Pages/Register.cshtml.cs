using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.UserService;
using System.ComponentModel.DataAnnotations;

namespace Views.Pages
{
    public class RegisterModel(IUserService userService) : PageModel
    {
        private readonly IUserService userService = userService;

        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;
        [BindProperty]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;
        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;
        [BindProperty]
        [Required(ErrorMessage = "Confirm password is required")]
        public string ConfirmPassword { get; set; } = null!;
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            if (Password != ConfirmPassword)
            {
                ViewData["ErrorMessage"] = "Passwords do not match";
                return Page();
            }
            return Page();
        }
    }
}
