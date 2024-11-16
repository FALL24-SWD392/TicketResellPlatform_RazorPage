using AspNetCoreHero.ToastNotification.Abstractions;
using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.UserService;
using Utils;

namespace Views.Pages.Manager
{
    public class IndexModel(IUserService userService, INotyfService notyfService) : PageModel
    {
        [BindProperty] public IList<User> Users { get; set; } = [];
        public async Task<IActionResult> OnGetAsync()
        {
            Users = await userService.GetAllAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveUserAsync(int id)
        {
            string? logedInUser = HttpContext.Session.GetString("LogedInUser");
            User? manager = logedInUser != null ? JsonUtil.ReadJsonItem<User>(logedInUser) : null;
            User userToRemove = await userService.GetAsync(id);
            if (manager == null || userToRemove == null)
            {
                notyfService.Error("User not found");
            }else if (manager.Id == userToRemove.Id)
            {
                notyfService.Error("You can't remove yourself");
            }
            else if (userToRemove.RoleId == 1)
            {
                notyfService.Error("You can't remove admin");
            }else if (userToRemove.RoleId == manager.RoleId)
            {
                notyfService.Error("You can't remove other manager");
            }else if( userToRemove.StatusId == 2)
            {
                notyfService.Error("User already removed");
            }
            else await userService.DeleteAsync(id);
            Users = await userService.GetAllAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostBanUserAsync(int id)
        {
            string? logedInUser = HttpContext.Session.GetString("LogedInUser");
            User? manager = logedInUser != null ? JsonUtil.ReadJsonItem<User>(logedInUser) : null;
            User userToRemove = await userService.GetAsync(id);
            if (manager == null || userToRemove == null)
            {
                notyfService.Error("User not found");
            }
            else if (manager.Id == userToRemove.Id)
            {
                notyfService.Error("You can't remove yourself");
            }
            else if (userToRemove.RoleId == 1)
            {
                notyfService.Error("You can't remove admin");
            }
            else if (userToRemove.RoleId == manager.RoleId)
            {
                notyfService.Error("You can't remove other manager");
            }
            else if (userToRemove.StatusId == 3)
            {
                notyfService.Error("User already banned");
            }
            else {
                userToRemove.StatusId = 3;
                await userService.UpdateAsync(userToRemove);
            }
            Users = await userService.GetAllAsync();
            return Page();
        }
    }
}
