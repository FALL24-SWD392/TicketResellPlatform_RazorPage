using Business;
using Daos.OrderDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private static OrderRepository? _instance;
        private OrderRepository() { }
        public static OrderRepository Instance
        {
            get
            {
                _instance ??= new();
                return _instance;
            }
        }
        public Task<Order?> AddAsync(Order order) => OrderDao.Instance.AddAsync(order);

        public Task<Order?> DeleteAsync(int id) => OrderDao.Instance.DeleteAsync(id);

        public Task<IList<Order>> GetAllAsync() => OrderDao.Instance.GetAllAsync();

        public Task<Order?> GetAsync(int id) => OrderDao.Instance.GetAsync(id);

        public Task<Order?> UpdateAsync(Order order) => OrderDao.Instance.UpdateAsync(order);
    }
}
