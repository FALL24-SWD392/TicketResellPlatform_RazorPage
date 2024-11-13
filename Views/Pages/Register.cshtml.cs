using Business;
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
            if ((await userService.GetAllAsync()).Any(u => u.Email.Equals(Email) || u.Username.Equals(Username)))
            {
                ViewData["ErrorMessage"] = "Email or Username has already existed!";
                return Page();
            }
            var user = new User
            {
                Email = Email,
                Username = Username,
                Password = Password,
                Avatar = "default.png",
                Rating = 0,
                Reputation = 100,
                RoleId = 3,
                StatusId = 1
            };
            await userService.AddAsync(user);

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Email", user.Email);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Password", string.Empty);
            HttpContext.Session.SetString("Avatar", user.Avatar);
            HttpContext.Session.SetString("Rating", user.Rating.ToString());
            HttpContext.Session.SetInt32("Reputation", user.Reputation.Value);
            HttpContext.Session.SetInt32("RoleId", user.RoleId);
            HttpContext.Session.SetInt32("StatusId", user.StatusId);
            HttpContext.Session.SetString("CreateAt", user.CreateAt.ToString());
            HttpContext.Session.SetString("UpdateAt", user.UpdateAt.ToString());

            return RedirectToPage("/Index");
        }
    }
}
