using Business;
using Repositories.FeedbackRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FeedbackService
{
    public class FeedbackService : IFeedbackService
    {
        private static FeedbackService? instance;
        private IFeedbackRepo feedbackRepo;
        private FeedbackService()
        {
            feedbackRepo = FeedbackRepo.Instance;
        }
        public static FeedbackService Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }
        public async Task<Feedback?> AddAsync(Feedback entity) => await feedbackRepo.AddAsync(entity);

        public async Task<Feedback?> DeleteAsync(int id) => await feedbackRepo.DeleteAsync(id);

        public async Task<IList<Feedback>> GetAllAsync() => await feedbackRepo.GetAllAsync();

        public async Task<Feedback?> GetAsync(int id) => await feedbackRepo.GetAsync(id);

        public async Task<Feedback?> UpdateAsync(Feedback entity) => await feedbackRepo.UpdateAsync(entity);
    }
}
