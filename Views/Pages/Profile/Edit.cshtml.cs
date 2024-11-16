using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.UserService;
using Utils;

namespace Views.Pages.Profile
{
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            this._userService = userService;
            this._webHostEnvironment = webHostEnvironment;
        }
        [BindProperty] public User Profile { get; set; } = null!;
        [BindProperty] public IFormFile Photo { get; set; } = null!;
        public async Task<IActionResult> OnGetAsync()
        {
            string? logedInUser = HttpContext.Session.GetString("LogedInUser");
            User? user = logedInUser != null ? JsonUtil.ReadJsonItem<User>(logedInUser) : null;
            Profile  = await _userService.GetAsync(user.Id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(User profile)
        {
            string avatar = "";
            if (Photo != null)
            {
                if (Profile.Avatar != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", Profile.Avatar);
                    System.IO.File.Delete(filePath);
                }
                avatar = ProcessUploadedFile();
            }
            string? logedInUser = HttpContext.Session.GetString("LogedInUser");
            User? user = logedInUser != null ? JsonUtil.ReadJsonItem<User>(logedInUser) : null;
            Profile = await _userService.GetAsync(user.Id);
            Profile.Email = profile.Email;
            Profile.Username = profile.Username;
            Profile.Avatar = avatar != "" ? avatar : Profile.Avatar;
            user = await _userService.UpdateAsync(Profile);
            string loginUser = JsonUtil.WriteJsonItem(user);
            HttpContext.Session.SetString("LogedInUser", loginUser);
            return Page();
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }

            }
            return "/images/" + uniqueFileName;

        }
    }
    
}
