using Business;
using Daos.FeedbackDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.FeedbackRepository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private static FeedbackRepository? instance;
        private readonly IFeedbackDao feedbackDao;
        private FeedbackRepository()
        {
            feedbackDao = FeedbackDao.Instance;
        }
        public static FeedbackRepository Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Feedback?> AddAsync(Feedback entity) => await feedbackDao.AddAsync(entity);

        public async Task<Feedback?> DeleteAsync(int id) => await feedbackDao.DeleteAsync(id);

        public async Task<IList<Feedback>> GetAllAsync() => await feedbackDao.GetAllAsync();

        public async Task<Feedback?> GetAsync(int id) => await feedbackDao.GetAsync(id);

        public async Task<Feedback?> UpdateAsync(Feedback entity) => await feedbackDao.UpdateAsync(entity);
    }
}
