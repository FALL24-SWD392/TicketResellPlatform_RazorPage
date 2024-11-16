using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Services.ChatService;

namespace Views.Pages.Chats
{
    public class IndexModel : PageModel
    {
        private readonly IChatService _chatService;

        public IndexModel(IChatService chatService)
        {
            _chatService = chatService;
        }
        public IList<Chatbox> Chatboxes { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync()
        {
            if (await _chatService.GetAllAsync() != null)
            {
                Chatboxes = await _chatService.GetAllAsync();
            }
            return Page();
        }
    }
}
