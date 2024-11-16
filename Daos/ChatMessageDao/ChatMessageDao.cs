using Business;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daos.ChatMessageDao
{
    public class ChatMessageDao : IChatMessageDao
    {
        private static ChatMessageDao? instance;
        private readonly TicketResellContext context;
        private ChatMessageDao()
        {
            context = new();
        }
        public static ChatMessageDao Instance { get { instance ??= new(); return instance; } }

        public async Task<ChatMessage?> AddAsync(ChatMessage entity)
        {
            await context.ChatMessages.AddAsync(entity);
            await context.SaveChangesAsync();
            context.Entry(entity).State = EntityState.Detached;

            return await context.ChatMessages.Include(cm => cm.Sender).Include(cm => cm.ChatBox).OrderByDescending(cm => cm.CreateAt).FirstOrDefaultAsync();
        }

        public Task<ChatMessage?> DeleteAsync(int id)
        {
            throw new NotSupportedException("This function is not supported");
        }

        public async Task<IList<ChatMessage>> GetAllAsync() => await context.ChatMessages.ToListAsync();

        public async Task<ChatMessage?> GetAsync(int id) => await context.ChatMessages.SingleOrDefaultAsync(c => c.Id == id);

        public async Task<ChatMessage?> UpdateAsync(ChatMessage entity)
        {
            context.ChatMessages.Update(entity);
            await context.SaveChangesAsync();
            context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
    }
}
