using Business;

namespace Services.UserService
{
    public interface IUserService : IService<User>
    {
        Task<User?> Login(string email, string password);
    }
}
