using Business;
using Daos.MembershipDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.MembershipRepository
{
    public class MembershipRepository : IMembershipRepository
    {
        private static MembershipRepository? _instance;
        private readonly IMembershipDao membershipDao;
        private MembershipRepository()
        {
            membershipDao = MembershipDao.Instance;
        }
        public static MembershipRepository Instance
        {
            get
            {
                _instance ??= new();
                return _instance;
            }
        }
        public Task<Membership?> AddAsync(Membership membership) => membershipDao.AddAsync(membership);

        public Task<Membership?> DeleteAsync(int id) => membershipDao.DeleteAsync(id);

        public Task<IList<Membership>> GetAllAsync() => membershipDao.GetAllAsync();

        public Task<Membership?> GetAsync(int id) => membershipDao.GetAsync(id);

        public Task<Membership?> UpdateAsync(Membership membership) => membershipDao.UpdateAsync(membership);
    }
}
