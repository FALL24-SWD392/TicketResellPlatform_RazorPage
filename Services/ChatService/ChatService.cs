using Business;
using Repositories.ChatboxRepository;
using Repositories.ChatMessageRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ChatService
{
    public class ChatService : IChatService
    {
        private static ChatService? instance;
        private readonly IChatboxRepository chatboxRepository;
        private readonly IChatMessageRepository chatMessageRepository;
        private ChatService()
        {
            chatboxRepository = ChatboxRepository.Instance;
            chatMessageRepository = ChatMessageRepository.Instance;
        }
        public static ChatService Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<ChatMessage?> CreateChatMessage(ChatMessage entity) => await chatMessageRepository.AddAsync(entity);
        public async Task<Chatbox?> AddAsync(Chatbox entity) => await chatboxRepository.AddAsync(entity);

        public async Task<Chatbox?> DeleteAsync(int id) => await chatboxRepository.DeleteAsync(id);

        public async Task<IList<Chatbox>> GetAllAsync() => await chatboxRepository.GetAllAsync();

        public async Task<Chatbox?> GetAsync(int id) => await chatboxRepository.GetAsync(id);

        public async Task<Chatbox?> UpdateAsync(Chatbox entity) => await chatboxRepository.UpdateAsync(entity);
    }
}
