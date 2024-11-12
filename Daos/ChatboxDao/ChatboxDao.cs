using Business;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daos.ChatboxDao
{
    public class ChatboxDao : IChatboxDao
    {
        private static ChatboxDao? instance;
        private TicketResellContext context;
        private ChatboxDao()
        {
            context = new();
        }
        public static ChatboxDao Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Chatbox?> AddAsync(Chatbox entity)
        {
            await context.Chatboxes.AddAsync(entity);
            await context.SaveChangesAsync();
            context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public Task<Chatbox?> DeleteAsync(int id)
        {
            throw new NotSupportedException("This function is not supported");
        }

        public async Task<IList<Chatbox>> GetAllAsync() => await context.Chatboxes.Include(c => c.Status).ToListAsync();

        public async Task<Chatbox?> GetAsync(int id) => await context.Chatboxes.Include(c => c.Status).SingleOrDefaultAsync(c => c.Id == id);

        public async Task<Chatbox?> UpdateAsync(Chatbox entity)
        {
            context.Chatboxes.Update(entity);
            await context.SaveChangesAsync();
            context.Entry(entity).State = EntityState.Detached;
            return await GetAsync(entity.Id);
        }
    }
}
