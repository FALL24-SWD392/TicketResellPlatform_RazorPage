using Business;
using Daos;
using Daos.UserDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private static UserRepository? instance;
        private readonly IUserDao dao;
        private UserRepository()
        {
            dao = UserDao.Instance;
        }
        public static UserRepository Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<User?> AddAsync(User entity)
            => await dao.AddAsync(entity);

        public async Task<User?> DeleteAsync(int id) 
            => await dao.DeleteAsync(id);

        public async Task<IList<User>> GetAllAsync()
            => await dao.GetAllAsync();

        public async Task<User?> GetAsync(int id)
            => await dao.GetAsync(id);

        public async Task<User?> UpdateAsync(User entity)
            => await dao.UpdateAsync(entity);
    }
}
