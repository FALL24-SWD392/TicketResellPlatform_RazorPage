using Business;
using Daos.TicketTypeDao;

namespace Repositories.TicketTypeRepository
{
    public class TicketTypeRepository : ITicketTypeRepository
    {
        private static TicketTypeRepository? instance;
        private readonly ITicketTypeDao dao;
        private TicketTypeRepository()
        {
            dao = TicketTypeDao.Instance;
        }
        public static TicketTypeRepository Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<TicketType?> AddAsync(TicketType entity)
            => await dao.AddAsync(entity);

        public async Task<TicketType?> DeleteAsync(int id)
            => await dao.DeleteAsync(id);

        public async Task<IList<TicketType>> GetAllAsync()
            => await dao.GetAllAsync();

        public async Task<TicketType?> GetAsync(int id)
            => await dao.GetAsync(id);

        public async Task<TicketType?> UpdateAsync(TicketType entity)
            => await dao.UpdateAsync(entity);
    }
}
