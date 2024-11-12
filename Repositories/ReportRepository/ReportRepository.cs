using Business;
using Daos.ReportDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ReportRepository
{
    public class ReportRepository : IReportRepository
    {
        private static ReportRepository? _instance;
        private readonly IReportDao reportDao;
        private ReportRepository()
        {
            reportDao = ReportDao.Instance;
        }
        public static ReportRepository Instance
        {
            get
            {
                _instance ??= new();
                return _instance;
            }
        }
        public Task<Report?> AddAsync(Report report) => reportDao.AddAsync(report);

        public Task<Report?> DeleteAsync(int id) => reportDao.DeleteAsync(id);

        public Task<IList<Report>> GetAllAsync() => reportDao.GetAllAsync();

        public Task<Report?> GetAsync(int id) => reportDao.GetAsync(id);

        public Task<Report?> UpdateAsync(Report report) => reportDao.UpdateAsync(report);
    }
}
