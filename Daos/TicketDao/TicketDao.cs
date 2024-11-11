using Business;
using Microsoft.EntityFrameworkCore;

namespace Daos.TicketDao
{
    public class TicketDao : ITicketDao
    {
        private static TicketDao? instance;
        private readonly TicketResellContext context;
        private TicketDao()
        {
            context = new();
        }
        public static TicketDao Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Ticket?> AddAsync(Ticket entity)
        {
            context.Tickets.Add(entity);
            await context.SaveChangesAsync();
            context.Entry(entity).State = EntityState.Detached;
            return await context.Tickets.Include(t => t.Type).Include(t => t.Status)
                .OrderByDescending(t => t.CreateAt).FirstOrDefaultAsync();
        }

        public async Task<Ticket?> DeleteAsync(int id)
        {
            Ticket ticket = await GetAsync(id) ?? throw new KeyNotFoundException("Ticket not found");
            ticket.StatusId = 6;
            return await UpdateAsync(ticket);
        }

        public async Task<IList<Ticket>> GetAllAsync()
            => await context.Tickets.Include(t => t.Type).Include(t => t.Status).ToListAsync();

        public async Task<Ticket?> GetAsync(int id)
            => await context.Tickets.Include(t => t.Type).Include(t => t.Status).FirstOrDefaultAsync(t => t.Id == id);

        public async Task<Ticket?> UpdateAsync(Ticket entity)
        {
            context.Tickets.Update(entity);
            await context.SaveChangesAsync();
            context.Entry(entity).State = EntityState.Detached;
            return entity;
        }
    }
}
