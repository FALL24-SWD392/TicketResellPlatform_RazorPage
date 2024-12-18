﻿using Business;
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
        private readonly TicketResellContext context;
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

        public async Task<IList<Chatbox>> GetAllAsync() 
            => await context.Chatboxes.Include(c => c.Status).Include(c => c.Buyer).Include(c => c.Seller).Include(c => c.Ticket).Include(c => c.ChatMessages).ToListAsync();

        public async Task<Chatbox?> GetAsync(int id) => await context.Chatboxes.Include(c => c.Status).Include(c => c.ChatMessages).Include(c => c.Buyer).Include(c => c.Seller).Include(c => c.Ticket).SingleOrDefaultAsync(c => c.Id == id);

        public async Task<List<Chatbox>> GetChatBoxOfUser(int userId) => await context.Chatboxes.Include(c => c.Status).Include(c => c.ChatMessages).Include(c => c.Buyer).Include(c => c.Seller).Include(c => c.Ticket).Where(c => c.SellerId == userId || c.BuyerId == userId).ToListAsync();

        public async Task<Chatbox?> UpdateAsync(Chatbox entity)
        {
            entity.Status = await context.ChatboxStatuses.FindAsync(entity.StatusId);
            context.Chatboxes.Update(entity);
            await context.SaveChangesAsync();
            context.Entry(entity).State = EntityState.Detached;
            return await GetAsync(entity.Id);
        }
    }
}
