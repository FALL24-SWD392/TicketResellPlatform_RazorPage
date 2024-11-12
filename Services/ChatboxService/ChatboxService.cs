using Business;
using Repositories.ChatboxRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ChatboxService
{
    public class ChatboxService : IChatboxService
    {
        private static ChatboxService? instance;
        private ChatboxRepo chatboxRepo;
        private ChatboxService()
        {
            chatboxRepo = ChatboxRepo.Instance;
        }
        public static ChatboxService Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Chatbox?> AddAsync(Chatbox entity) => await chatboxRepo.AddAsync(entity);

        public async Task<Chatbox?> DeleteAsync(int id) => await chatboxRepo.DeleteAsync(id);

        public async Task<IList<Chatbox>> GetAllAsync() => await chatboxRepo.GetAllAsync();

        public async Task<Chatbox?> GetAsync(int id) => await chatboxRepo.GetAsync(id);

        public async Task<Chatbox?> UpdateAsync(Chatbox entity) => await chatboxRepo.UpdateAsync(entity);
    }
}
