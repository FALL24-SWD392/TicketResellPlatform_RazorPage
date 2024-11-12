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
        public static FeedbackRepository Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Feedback?> AddAsync(Feedback entity) => await FeedbackDao.Instance.AddAsync(entity);

        public async Task<Feedback?> DeleteAsync(int id)=> await FeedbackDao.Instance.DeleteAsync(id);

        public async Task<IList<Feedback>> GetAllAsync() => await FeedbackDao.Instance.GetAllAsync();

        public async Task<Feedback?> GetAsync(int id) => await FeedbackDao.Instance.GetAsync(id);

        public async Task<Feedback?> UpdateAsync(Feedback entity) => await FeedbackDao.Instance.UpdateAsync(entity);
    }
}
