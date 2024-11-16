using Business;
using Utils;
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

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                User? user = await userService.Login(Email, Password);
                string userJson = JsonUtil.WriteJsonItem<User>(user);
                HttpContext.Session.SetString("LogedInUser", userJson);
            }
            catch (Exception e)
            {
                ViewData["ErrorMessage"] = e.Message;
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
