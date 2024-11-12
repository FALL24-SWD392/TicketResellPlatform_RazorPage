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
        public Task<Membership?> AddAsync(Membership membership) => MembershipDao.Instance.AddAsync(membership);

        public Task<Membership?> DeleteAsync(int id) => MembershipDao.Instance.DeleteAsync(id);

        public Task<IList<Membership>> GetAllAsync() => MembershipDao.Instance.GetAllAsync();

        public Task<Membership?> GetAsync(int id) => MembershipDao.Instance.GetAsync(id);

        public Task<Membership?> UpdateAsync(Membership membership) => MembershipDao.Instance.UpdateAsync(membership);
    }
}
