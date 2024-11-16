using Business;

namespace Repositories.UserRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUsreByEmail(string email);
    }
}
