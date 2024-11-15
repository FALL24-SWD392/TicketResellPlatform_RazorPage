using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Views.Pages.Manager
{
    public class IndexModel : PageModel
    {
        [BindProperty] public IList<User> Users { get; set; } = [];
        public void OnGet()
        {
            Users = [
                new () {
                    Id = 1, 
                    Username = "John Doe", 
                    Email = "email@example.com", 
                    Avatar = "/images/static/user-avatar.png", 
                    CreateAt = DateTime.UtcNow,
                    Role = new Role { Id = 1, Name = "User" },
                    StatusId = 1,
                    Status = new UserStatus { Id = 1, Status = "active" },
                },
                new () {
                    Id = 1, 
                    Username = "John Doe", 
                    Email = "email@example.com", 
                    Avatar = "/images/static/user-avatar.png", 
                    CreateAt = DateTime.UtcNow,
                    Role = new Role { Id = 1, Name = "User" },
                    StatusId = 1,
                    Status = new UserStatus { Id = 1, Status = "active" },
                },
                new () {
                    Id = 1, 
                    Username = "John Doe", 
                    Email = "email@example.com", 
                    Avatar = "/images/static/user-avatar.png", 
                    CreateAt = DateTime.UtcNow,
                    Role = new Role { Id = 1, Name = "User" },
                    StatusId = 1,
                    Status = new UserStatus { Id = 1, Status = "active" },
                },
                new () {
                    Id = 1, 
                    Username = "John Doe", 
                    Email = "email@example.com", 
                    Avatar = "/images/static/user-avatar.png", 
                    CreateAt = DateTime.UtcNow,
                    Role = new Role { Id = 1, Name = "User" },
                    StatusId = 1,
                    Status = new UserStatus { Id = 1, Status = "active" },
                },
                new () {
                    Id = 1, 
                    Username = "John Doe", 
                    Email = "email@example.com", 
                    Avatar = "/images/static/user-avatar.png", 
                    CreateAt = DateTime.UtcNow,
                    Role = new Role { Id = 1, Name = "User" },
                    StatusId = 2,
                    Status = new UserStatus { Id = 2, Status = "removed" },
                },
                new () {
                    Id = 1, 
                    Username = "John Doe", 
                    Email = "email@example.com", 
                    Avatar = "/images/static/user-avatar.png", 
                    CreateAt = DateTime.UtcNow,
                    Role = new Role { Id = 1, Name = "User" },
                    StatusId = 1,
                    Status = new UserStatus { Id = 1, Status = "active" },
                },
                new () {
                    Id = 1, 
                    Username = "John Doe", 
                    Email = "email@example.com", 
                    Avatar = "/images/static/user-avatar.png", 
                    CreateAt = DateTime.UtcNow,
                    Role = new Role { Id = 1, Name = "User" },
                    StatusId = 1,
                    Status = new UserStatus { Id = 1, Status = "active" },
                },
                new () {
                    Id = 1, 
                    Username = "John Doe", 
                    Email = "email@example.com", 
                    Avatar = "/images/static/user-avatar.png", 
                    CreateAt = DateTime.UtcNow,
                    Role = new Role { Id = 1, Name = "User" },
                    StatusId = 1,
                    Status = new UserStatus { Id = 1, Status = "active" },
                },
                new () {
                    Id = 1, 
                    Username = "John Doe", 
                    Email = "email@example.com", 
                    Avatar = "/images/static/user-avatar.png", 
                    CreateAt = DateTime.UtcNow,
                    Role = new Role { Id = 1, Name = "User" },
                    StatusId = 3,
                    Status = new UserStatus { Id = 3, Status = "banned" },
                },
                new () {
                    Id = 1, 
                    Username = "John Doe", 
                    Email = "email@example.com", 
                    Avatar = "/images/static/user-avatar.png", 
                    CreateAt = DateTime.UtcNow,
                    Role = new Role { Id = 1, Name = "User" },
                    StatusId = 2,
                    Status = new UserStatus { Id = 2, Status = "removed" },
                },
            ];
        }
    }
}
