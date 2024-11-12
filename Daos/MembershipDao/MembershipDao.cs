using Business;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daos.MembershipDao
{
    public class MembershipDao : IMembershipDao
    {
        private static MembershipDao? instance;
        private readonly TicketResellContext _context;

        private MembershipDao()
        {
            _context = new TicketResellContext();
        }

        public static MembershipDao Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Membership?> AddAsync(Membership membership)
        {
            await _context.Memberships.AddAsync(membership);
            await _context.SaveChangesAsync();
            _context.Entry(membership).State = EntityState.Detached;

            return membership;
        }

        public async Task<Membership?> DeleteAsync(int id)
        {
            var membership = await _context.Memberships
                .Include(m => m.Owner)
                .Include(m => m.Status)
                .Include(m => m.Supscription)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (membership == null)
            {
                throw new ArgumentNullException(nameof(membership));
            }

            membership.StatusId = 2;
            _context.Memberships.Update(membership);
            await _context.SaveChangesAsync();
            _context.Entry(membership).State = EntityState.Detached;

            return membership;
        }

        public async Task<IList<Membership>> GetAllAsync()
        {
            return await _context.Memberships
                .Include(m => m.Owner)
                .Include(m => m.Status)
                .Include(m => m.Supscription)
                .ToListAsync();
        }

        public async Task<Membership?> GetAsync(int id)
        {
            return await _context.Memberships
                .Include(m => m.Owner)
                .Include(m => m.Status)
                .Include(m => m.Supscription)
                .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Membership?> UpdateAsync(Membership membership)
        {
            var memb = await _context.Memberships
                .Include(m => m.Owner)
                .Include(m => m.Status)
                .Include(m => m.Supscription)
                .SingleOrDefaultAsync(m => m.Id == membership.Id);
            if (memb == null)
            {
                throw new ArgumentNullException(nameof(membership));
            }
            else
            {
                _context.Memberships.Update(membership);
                await _context.SaveChangesAsync();
                _context.Entry(membership).State = EntityState.Detached;

                return membership;
            }
        }
    }
}
