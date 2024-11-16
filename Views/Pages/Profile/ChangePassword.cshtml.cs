using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.UserService;
using Utils;

namespace Views.Pages.Profile
{
    public class ChangePasswordModel(IUserService userService) : PageModel
    {
        [BindProperty] public User Profile { get; set; } = null!;
        [BindProperty] public string OldPassword { get; set; } = string.Empty;
        [BindProperty] public string NewPassword { get; set; } = string.Empty;
        [BindProperty] public string ConfirmPassword { get; set; } = string.Empty;
        public async Task<IActionResult> OnGetAsync()
        {
            string? logedInUser = HttpContext.Session.GetString("LogedInUser");
            User? user = logedInUser != null ? JsonUtil.ReadJsonItem<User>(logedInUser) : null;
            Profile = await userService.GetAsync(user.Id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string? logedInUser = HttpContext.Session.GetString("LogedInUser");
            User? user = logedInUser != null ? JsonUtil.ReadJsonItem<User>(logedInUser) : null;
            Profile = await userService.GetAsync(user.Id);
            if (!BCrypt.Net.BCrypt.Verify(OldPassword, Profile.Password))
            {
                ModelState.AddModelError("OldPassword", "Old password is incorrect");
                return Page();
            }
            if (NewPassword != ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Password and confirm password do not match");
                return Page();
            }
            Profile.Password = BCrypt.Net.BCrypt.HashPassword(NewPassword);
            user = await userService.UpdateAsync(Profile);
            string loginUser = JsonUtil.WriteJsonItem(user);
            HttpContext.Session.SetString("LogedInUser", loginUser);
            return Page();
        }
    }
}
