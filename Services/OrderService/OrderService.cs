using Business;
using Repositories.OrderRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService()
        {
            this.orderRepository = new OrderRepository();
        }

        public Task<Order?> AddAsync(Order order) => orderRepository.AddAsync(order);

        public Task<Order?> DeleteAsync(int id) => orderRepository.DeleteAsync(id);

        public Task<IList<Order>> GetAllAsync() => orderRepository.GetAllAsync();

        public Task<Order?> GetAsync(int id) => orderRepository.GetAsync(id);

        public Task<Order?> UpdateAsync(Order order) => orderRepository.UpdateAsync(order);
    }
}
