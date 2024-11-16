using BCrypt.Net;
using Business;
using Repositories.UserRepository;

namespace Services.UserService
{
    public class UserService : IUserService
    {
        private static UserService? instance;
        private readonly IUserRepository userRepo;
        private UserService()
        {
            userRepo = UserRepository.Instance;
        }
        public static UserService Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<User?> AddAsync(User entity)
            => await userRepo.AddAsync(entity);

        public async Task<User?> DeleteAsync(int id)
            => await userRepo.DeleteAsync(id);

        public async Task<IList<User>> GetAllAsync()
            => await userRepo.GetAllAsync();

        public async Task<User?> GetAsync(int id)
            => await userRepo.GetAsync(id);

        public async Task<User?> Login(string email, string password)
        {
            User? user = await userRepo.GetUsreByEmail(email) ?? throw new KeyNotFoundException("Email not found");
            if(BCrypt.Net.BCrypt.Verify(password, user?.Password))
                return user; 
            else throw new ArgumentException("Password is incorrect");
        }

        public async Task<User?> UpdateAsync(User entity)
            => await userRepo.UpdateAsync(entity);
    }
}
