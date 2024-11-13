using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Views.Pages.Tickets
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();
        public void OnGet()
        {
            Tickets = [
                new(){
                    Image = "https://placehold.co/400",
                    Title = "Ticket 1",
                    Description = "Description 1",
                    Price = 100
                },
                new(){
                    Image = "https://placehold.co/400",
                    Title = "Ticket 2",
                    Description = "Description 2",
                    Price = 100
                },
                new(){
                    Image = "https://placehold.co/400",
                    Title = "Ticket 2",
                    Description = "Description 2",
                    Price = 100
                },
                new(){
                    Image = "https://placehold.co/400",
                    Title = "Ticket 2",
                    Description = "Description 2",
                    Price = 100
                },
                new(){
                    Image = "https://placehold.co/400",
                    Title = "Ticket 2",
                    Description = "Description 2",
                    Price = 100
                }
            ];
        }
    }
}
