using Business;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daos.ReportDao
{
    public class ReportDao : IReportDao
    {
        private static ReportDao? instance;
        private readonly TicketResellContext _context;

        private ReportDao()
        {
            _context = new TicketResellContext();
        }

        public static ReportDao Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Report?> AddAsync(Report report)
        {
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
            _context.Entry(report).State = EntityState.Detached;

            return report;
        }

        public Task<Report?> DeleteAsync(int id)
        {
            throw new NotSupportedException("Delete report function is not support yet!");
        }

        public async Task<IList<Report>> GetAllAsync()
        {
            return await _context.Reports
                .Include(r => r.Order)
                .Include(r => r.Sender)
                .Include(r => r.Status)
                .ToListAsync();
        }
        
        public async Task<Report?> GetAsync(int id)
        {
            return await _context.Reports
                .Include(r => r.Order)
                .Include(r => r.Sender)
                .Include(r => r.Status)
                .SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Report?> UpdateAsync(Report report)
        {
            var rep = await _context.Reports
                .Include(r => r.Order)
                .Include(r => r.Sender)
                .Include(r => r.Status).SingleOrDefaultAsync(r => r.Id == report.Id);
            if (rep == null)
            {
                throw new ArgumentNullException(nameof(report));
            }
            else
            {
                _context.Reports.Update(report);
                await _context.SaveChangesAsync();
                _context.Entry(report).State = EntityState.Detached;

                return report;
            }    
        }
    }
}
