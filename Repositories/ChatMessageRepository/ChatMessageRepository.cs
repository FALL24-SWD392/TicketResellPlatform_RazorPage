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
        public static ChatMessageRepository Instance { get { instance ??= new(); return instance; } }

        public async Task<ChatMessage?> AddAsync(ChatMessage entity) => await ChatMessageDao.Instance.AddAsync(entity);

        public async Task<ChatMessage?> DeleteAsync(int id) => await ChatMessageDao.Instance.DeleteAsync(id);

        public async Task<IList<ChatMessage>> GetAllAsync() => await ChatMessageDao.Instance.GetAllAsync();

        public async Task<ChatMessage?> GetAsync(int id) => await ChatMessageDao.Instance.GetAsync(id);

        public async Task<ChatMessage?> UpdateAsync(ChatMessage entity) => await ChatMessageDao.Instance.UpdateAsync(entity);
    }
}
