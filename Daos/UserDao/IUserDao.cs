using Business;

namespace Daos.UserDao
{
    public interface IUserDao : IDao<User>
    {
        Task<User?> GetUsreByEmail(string email);
    }
}