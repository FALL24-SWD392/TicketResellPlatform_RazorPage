using Business;
using Daos.SubsciptionDao;
using Repositories.SubscriptionRepository;

namespace Services.SubscriptionService
{
    public class SubscriptionService : ISubscriptionService
    {
        private static SubscriptionService? instance;
        private readonly ISubscriptionRepository subscriptionRepo;
        private SubscriptionService()
        {
            subscriptionRepo = SubscriptionRepository.Instance;
        }
        public static SubscriptionService Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Subcription?> AddAsync(Subcription entity)
             => await subscriptionRepo.AddAsync(entity);

        public async Task<Subcription?> DeleteAsync(int id)
            => await subscriptionRepo.DeleteAsync(id);  

        public async Task<IList<Subcription>> GetAllAsync()
            => await subscriptionRepo.GetAllAsync();

        public async Task<Subcription?> GetAsync(int id)
            => await subscriptionRepo.GetAsync(id);

        public async Task<Subcription?> UpdateAsync(Subcription entity)
            => await subscriptionRepo.UpdateAsync(entity);
    }
}
