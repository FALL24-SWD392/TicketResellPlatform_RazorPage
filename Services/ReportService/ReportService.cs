using Business;
using Repositories.OrderRepository;
using Repositories.ReportRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ReportService
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository reportRepository;

        public ReportService()
        {
            this.reportRepository = new ReportRepository();
        }

        public Task<Report?> AddAsync(Report report) => reportRepository.AddAsync(report);

        public Task<Report?> DeleteAsync(int id) => reportRepository.DeleteAsync(id);

        public Task<IList<Report>> GetAllAsync() => reportRepository.GetAllAsync();

        public Task<Report?> GetAsync(int id) => reportRepository.GetAsync(id);

        public Task<Report?> UpdateAsync(Report report) => reportRepository.UpdateAsync(report);
    }
}
