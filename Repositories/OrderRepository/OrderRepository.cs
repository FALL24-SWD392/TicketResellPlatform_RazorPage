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
        private readonly IOrderDao orderDao;
        private OrderRepository()
        {
            orderDao = OrderDao.Instance;
        }
        public static OrderRepository Instance
        {
            get
            {
                _instance ??= new();
                return _instance;
            }
        }
        public Task<Order?> AddAsync(Order order) => orderDao.AddAsync(order);

        public Task<Order?> DeleteAsync(int id) => orderDao.DeleteAsync(id);

        public Task<IList<Order>> GetAllAsync() => orderDao.GetAllAsync();

        public Task<Order?> GetAsync(int id) => orderDao.GetAsync(id);

        public Task<Order?> UpdateAsync(Order order) => orderDao.UpdateAsync(order);
    }
}
