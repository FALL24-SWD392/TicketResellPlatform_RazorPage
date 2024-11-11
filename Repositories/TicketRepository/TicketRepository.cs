using Business;
using Daos.TicketDao;

namespace Repositories.TicketRepository
{
    public class TicketRepository : ITicketRepository
    {
        private static TicketRepository? instance;
        private readonly ITicketDao dao;
        private TicketRepository()
        {
            dao = TicketDao.Instance;
        }
        private static TicketRepository Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Ticket?> AddAsync(Ticket entity)
            => await dao.AddAsync(entity);

        public async Task<Ticket?> DeleteAsync(int id)
            => await dao.DeleteAsync(id);

        public async Task<IList<Ticket>> GetAllAsync()
            => await dao.GetAllAsync();

        public async Task<Ticket?> GetAsync(int id)
            => await dao.GetAsync(id);

        public async Task<Ticket?> UpdateAsync(Ticket entity)
            => await dao.UpdateAsync(entity);
    }
}
