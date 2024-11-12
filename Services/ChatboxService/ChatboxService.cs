using Business;
using Repositories.ChatboxRepository;
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
        private readonly ChatboxRepository chatboxRepo;
        private ChatboxService()
        {
            chatboxRepo = ChatboxRepository.Instance;
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
