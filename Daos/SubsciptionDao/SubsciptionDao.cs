using Business;
using Microsoft.EntityFrameworkCore;

namespace Daos.SubsciptionDao
{
    public class SubsciptionDao : ISubscriptionDao
    {
        private static SubsciptionDao? instance;
        private readonly TicketResellContext context;
        private SubsciptionDao()
        {
            context = new();
        }
        public static SubsciptionDao Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Subcription?> AddAsync(Subcription entity)
        {
            context.Subcriptions.Add(entity);
            await context.SaveChangesAsync();
            context.Entry(entity).State = EntityState.Detached;
            return await context.Subcriptions.OrderByDescending(s => s.Id).FirstOrDefaultAsync();
        }

        public async Task<Subcription?> DeleteAsync(int id)
        {
            Subcription sub = await GetAsync(id) ?? throw new KeyNotFoundException("Subscription not found");
            sub.Description = "Decrepated" + sub.Description;
            return await UpdateAsync(sub);
        }

        public async Task<IList<Subcription>> GetAllAsync()
            => await context.Subcriptions.ToListAsync();

        public async Task<Subcription?> GetAsync(int id)
            => await context.Subcriptions.FirstOrDefaultAsync(s => s.Id == id);

        public async Task<Subcription?> UpdateAsync(Subcription entity)
        {
            context.Subcriptions.Update(entity);
            await context.SaveChangesAsync();
            context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
    }
}
