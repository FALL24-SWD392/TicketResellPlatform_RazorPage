using Business;
using Daos.ChatboxDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ChatboxRepository
{
    public class ChatboxRepository : IChatboxRepository
    {
        private static ChatboxRepository? instance;
        private readonly IChatboxDao chatboxDao;
        private ChatboxRepository()
        {
            chatboxDao = ChatboxDao.Instance;
        }
        public static ChatboxRepository Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Chatbox?> AddAsync(Chatbox entity) => await chatboxDao.AddAsync(entity);

        public async Task<Chatbox?> DeleteAsync(int id) => await chatboxDao.DeleteAsync(id);

        public async Task<IList<Chatbox>> GetAllAsync() => await chatboxDao.GetAllAsync();

        public async Task<Chatbox?> GetAsync(int id) => await chatboxDao.GetAsync(id);

        public async Task<List<Chatbox>> GetChatBoxOfUser(int UserId) => await chatboxDao.GetChatBoxOfUser(UserId);

        public async Task<Chatbox?> UpdateAsync(Chatbox entity) => await chatboxDao.UpdateAsync(entity);
    }
}
