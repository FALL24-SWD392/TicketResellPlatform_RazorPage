using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Services.ChatService;
using Utils;

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
            User user = JsonUtil.ReadJsonItem<User>(HttpContext.Session.GetString("LogedInUser"));

            if (await _chatService.GetChatboxOfUser(user.Id) != null)
            {
                Chatboxes = await _chatService.GetChatboxOfUser(user.Id);
            }
            return Page();
        }
    }
}
