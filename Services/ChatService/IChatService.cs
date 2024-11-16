using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ChatService
{
    public interface IChatService : IService<Chatbox>
    {
        public Task<ChatMessage?> CreateChatMessage(ChatMessage entity);
        public Task<List<Chatbox>> GetChatboxOfUser(int userId);
    }
}
