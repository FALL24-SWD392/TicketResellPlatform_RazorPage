using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Views.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty] public IList<Ticket> Tickets { get; set; } = new List<Ticket>();

        public void OnGet()
        {
            Tickets =
            [
                new()
                {
                    Id = 1,
                    Title = "Ticket 1",
                    Description = "Description 1",
                    Image = "https://viwo.vn/wp-content/uploads/2024/05/to-chuc-su-kien-event-la-gi-1-1170x658.jpg",
                    Price = 1000,
                    ExpiryDate = DateTime.Now
                },
                new()
                {
                    Id = 2,
                    Title = "Ticket 2",
                    Description = "Description 2",
                    Image = "https://viwo.vn/wp-content/uploads/2024/05/to-chuc-su-kien-event-la-gi-1-1170x658.jpg",
                    Price = 10000,
                    ExpiryDate = DateTime.Now
                },
                new (){
                    Id = 3,
                    Title = "Ticket 3",
                    Description = "Description 3",
                    Image = "https://viwo.vn/wp-content/uploads/2024/05/to-chuc-su-kien-event-la-gi-1-1170x658.jpg",
                    Price = 200000,
                    ExpiryDate = DateTime.Now
                },
                new (){
                    Id = 4,
                    Title = "Ticket 4",
                    Description = "Description 4",
                    Image = "https://viwo.vn/wp-content/uploads/2024/05/to-chuc-su-kien-event-la-gi-1-1170x658.jpg",
                    Price = 3000000,
                    ExpiryDate = DateTime.Now
                },
                new (){
                    Id = 5,
                    Title = "Ticket 5",
                    Description = "Description 5",
                    Image = "https://viwo.vn/wp-content/uploads/2024/05/to-chuc-su-kien-event-la-gi-1-1170x658.jpg",
                    Price = 4000000,
                    ExpiryDate = DateTime.Now
                },
                new ()
                {
                    Id = 6,
                    Title = "Xem Phim",
                    Description = "Xem phim ngay bay gio di",
                    Image = "https://viwo.vn/wp-content/uploads/2024/05/to-chuc-su-kien-event-la-gi-1-1170x658.jpg",
                    Price = 10000000,
                    ExpiryDate = DateTime.Now
                },
                new ()
                {
                Id = 7,
                Title = "Xem Phim",
                Description = "Xem phim ngay bay gio di",
                Image = "https://viwo.vn/wp-content/uploads/2024/05/to-chuc-su-kien-event-la-gi-1-1170x658.jpg",
                Price = 10000000,
                ExpiryDate = DateTime.Now
                },
                new ()
                {
                    Id = 8,
                    Title = "Xem Phim",
                    Description = "Xem phim ngay bay gio di",
                    Image = "https://viwo.vn/wp-content/uploads/2024/05/to-chuc-su-kien-event-la-gi-1-1170x658.jpg",
                    Price = 10000000,
                    ExpiryDate = DateTime.Now
                }
            ];
        }
    }
}