using Business;
using Daos.ChatMessageDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ChatMessageRepository
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private static ChatMessageRepository? instance;
        private readonly IChatMessageDao chatMessageDao;
        public ChatMessageRepository()
        {
            chatMessageDao = ChatMessageDao.Instance;
        }
        public static ChatMessageRepository Instance { get { instance ??= new(); return instance; } }

        public async Task<ChatMessage?> AddAsync(ChatMessage entity) => await chatMessageDao.AddAsync(entity);

        public async Task<ChatMessage?> DeleteAsync(int id) => await chatMessageDao.DeleteAsync(id);

        public async Task<IList<ChatMessage>> GetAllAsync() => await chatMessageDao.GetAllAsync();

        public async Task<ChatMessage?> GetAsync(int id) => await chatMessageDao.GetAsync(id);

        public async Task<ChatMessage?> UpdateAsync(ChatMessage entity) => await chatMessageDao.UpdateAsync(entity);
    }
}
