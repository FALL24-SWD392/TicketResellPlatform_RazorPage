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
        private ReportRepository() { }
        public static ReportRepository Instance
        {
            get
            {
                _instance ??= new();
                return _instance;
            }
        }
        public Task<Report?> AddAsync(Report report) => ReportDao.Instance.AddAsync(report);

        public Task<Report?> DeleteAsync(int id) => ReportDao.Instance.DeleteAsync(id);

        public Task<IList<Report>> GetAllAsync() => ReportDao.Instance.GetAllAsync();

        public Task<Report?> GetAsync(int id) => ReportDao.Instance.GetAsync(id);

        public Task<Report?> UpdateAsync(Report report) => ReportDao.Instance.UpdateAsync(report);
    }
}
