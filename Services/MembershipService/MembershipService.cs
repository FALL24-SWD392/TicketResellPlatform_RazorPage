using Business;
using Repositories.MembershipRepository;
using Repositories.ReportRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MembershipService
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository membershipRepository;
        private static MembershipService? _instance;
        private MembershipService()
        {
            membershipRepository = MembershipRepository.Instance;
        }

        public Task<Membership?> AddAsync(Membership membership) => membershipRepository.AddAsync(membership);

        public Task<Membership?> DeleteAsync(int id) => membershipRepository.DeleteAsync(id);

        public Task<IList<Membership>> GetAllAsync() => membershipRepository.GetAllAsync();

        public Task<Membership?> GetAsync(int id) => membershipRepository.GetAsync(id);

        public Task<Membership?> UpdateAsync(Membership membership) => membershipRepository.UpdateAsync(membership);
    }
}
