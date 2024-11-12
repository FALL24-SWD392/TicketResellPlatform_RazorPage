using Business;
using Daos.ChatboxDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ChatboxRepo
{
    public class ChatboxRepo : IChatboxRepo
    {
        private static ChatboxRepo? instance;
        public static ChatboxRepo Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Chatbox?> AddAsync(Chatbox entity) => await ChatboxDao.Instance.AddAsync(entity);

        public async Task<Chatbox?> DeleteAsync(int id) => await ChatboxDao.Instance.DeleteAsync(id);

        public async Task<IList<Chatbox>> GetAllAsync() => await ChatboxDao.Instance.GetAllAsync();

        public async Task<Chatbox?> GetAsync(int id) => await ChatboxDao.Instance.GetAsync(id);

        public async Task<Chatbox?> UpdateAsync(Chatbox entity) => await ChatboxDao.Instance.UpdateAsync(entity);
    }
}
