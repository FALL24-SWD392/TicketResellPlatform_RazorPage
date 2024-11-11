using Business;
using Microsoft.EntityFrameworkCore;

namespace Daos.TicketTypeDao
{
    public class TicketTypeDao : ITicketTypeDao
    {
        private static TicketTypeDao? instance;
        private readonly TicketResellContext context;
        private TicketTypeDao()
        {
            context = new();
        }
        public static TicketTypeDao Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public Task<TicketType?> AddAsync(TicketType entity)
        {
            throw new NotSupportedException("This function is not supported");
        }

        public Task<TicketType?> DeleteAsync(int id)
        {
            throw new NotSupportedException("This function is not supported");
        }

        public async Task<IList<TicketType>> GetAllAsync()
            => await context.TicketTypes.ToListAsync();

        public Task<TicketType?> GetAsync(int id)
        {
            throw new NotSupportedException("This function is not supported");
        }

        public Task<TicketType?> UpdateAsync(TicketType entity)
        {
            throw new NotSupportedException("This function is not supported");
        }
    }
}
