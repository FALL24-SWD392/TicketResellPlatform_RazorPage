using Business;
using Daos.SubsciptionDao;

namespace Repositories.SubscriptionRepository
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private static SubscriptionRepository instance;
        private readonly ISubscriptionDao dao;
        private SubscriptionRepository()
        {
            dao = SubscriptionDao.Instance;
        }
        private static SubscriptionRepository Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Subcription?> AddAsync(Subcription entity)
            => await dao.AddAsync(entity);

        public async Task<Subcription?> DeleteAsync(int id)
            => await dao.DeleteAsync(id);

        public async Task<IList<Subcription>> GetAllAsync()
            => await dao.GetAllAsync();

        public async Task<Subcription?> GetAsync(int id)
            => await dao.GetAsync(id);

        public async Task<Subcription?> UpdateAsync(Subcription entity)
            => await dao.UpdateAsync(entity);
    }
}
