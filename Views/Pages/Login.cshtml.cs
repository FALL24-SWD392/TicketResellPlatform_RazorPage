using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.UserService;
using System.ComponentModel.DataAnnotations;

namespace Views.Pages
{
    public class LoginModel(IUserService userService) : PageModel
    {
        private readonly IUserService userService = userService;

        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;
        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var user = (await userService.GetAllAsync()).FirstOrDefault(u => u.Email.Equals(Email) && u.Password.Equals(Password) && u.StatusId == 1);
            if (user == null)
            {
                ViewData["ErrorMessage"] = "Email or Password is incorrect!";
                return Page();
            }

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
