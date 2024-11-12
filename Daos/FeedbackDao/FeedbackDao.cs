using Business;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daos.FeedbackDao
{
    public class FeedbackDao : IFeedbackDao
    {
        private static FeedbackDao? instance;
        private TicketResellContext context;
        private FeedbackDao()
        {
            context = new();
        }
        public static FeedbackDao Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Feedback?> AddAsync(Feedback entity)
        {
            await context.Feedbacks.AddAsync(entity);
            await context.SaveChangesAsync();
            context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public Task<Feedback?> DeleteAsync(int id)
        {
            throw new NotSupportedException("This function is not supported");
        }

        public async Task<IList<Feedback>> GetAllAsync() => await context.Feedbacks.ToListAsync();

        public async Task<Feedback?> GetAsync(int id) => await context.Feedbacks.SingleOrDefaultAsync(f => f.Id == id);

        public async Task<Feedback?> UpdateAsync(Feedback entity)
        {
            context.Feedbacks.Update(entity);
            await context.SaveChangesAsync();
            context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
    }
}
