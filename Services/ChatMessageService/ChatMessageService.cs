using Business;
using Repositories.ChatMessageRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ChatMessageService
{
    public class ChatMessageService : IChatMessageService
    {
        private static ChatMessageService? instance;
        private ChatMessageRepository messageRepo;
        private ChatMessageService()
        {
            messageRepo = ChatMessageRepository.Instance;
        }
        public static ChatMessageService Instance { get { instance ??= new(); return instance; } }

        public async Task<ChatMessage?> AddAsync(ChatMessage entity) => await messageRepo.AddAsync(entity);

        public async Task<ChatMessage?> DeleteAsync(int id) => await messageRepo.DeleteAsync(id);

        public async Task<IList<ChatMessage>> GetAllAsync() => await messageRepo.GetAllAsync();

        public async Task<ChatMessage?> GetAsync(int id) => await messageRepo.GetAsync(id);

        public async Task<ChatMessage?> UpdateAsync(ChatMessage entity) => await messageRepo.UpdateAsync(entity);
    }
}
