using Business;
using Microsoft.EntityFrameworkCore;

namespace Daos.UserDao
{
    public class UserDao : IUserDao
    {
        private static UserDao? instance;
        private readonly TicketResellContext context;
        private UserDao()
        {
            context = new();
        }
        public static UserDao Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<User?> AddAsync(User entity)
        {
            context.Users.Add(entity);
            await context.SaveChangesAsync();
            context.Entry(entity).State = EntityState.Detached;
            User? user = await context.Users.Include(u => u.Role).Include(u => u.Status)
                .OrderByDescending(u => u.CreateAt).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User?> DeleteAsync(int id)
        {
            User user = await GetAsync(id) ?? throw new KeyNotFoundException("User not found");
            user.StatusId = 2;
            return await UpdateAsync(user);
        }

        public async Task<IList<User>> GetAllAsync()
            => await context.Users.Include(u => u.Role).Include(u => u.Status).ToListAsync();

        public async Task<User?> GetAsync(int id)
            => await context.Users.Include(u => u.Role).Include(u => u.Status).FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User?> GetUsreByEmail(string email)
            => await context.Users.Include(u => u.Role).Include(u => u.Status).FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User?> UpdateAsync(User entity)
        {
            entity.UpdateAt = DateTime.Now;
            entity.Status = await context.UserStatuses.FirstOrDefaultAsync(s => s.Id == entity.StatusId);
            context.Users.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}