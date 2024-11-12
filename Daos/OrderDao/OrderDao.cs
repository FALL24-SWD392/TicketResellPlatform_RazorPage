using Business;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daos.OrderDao
{
    public class OrderDao : IOrderDao
    {
        private static OrderDao? instance;
        private readonly TicketResellContext _context;

        private OrderDao()
        {
            _context = new TicketResellContext();
        }

        public static OrderDao Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Order?> AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            _context.Entry(order).State = EntityState.Detached;
            return order;
        }

        public Task<Order?> DeleteAsync(int id)
        {
            throw new NotSupportedException("Delete order function is not support yet!");
        }

        public async Task<IList<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.ChatBox)
                .Include(o => o.Feedbacks)
                .Include(o => o.Reports)
                .ToListAsync();
        }

        public async Task<Order?> GetAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.ChatBox)
                .Include(o => o.Feedbacks)
                .Include(o => o.Reports).SingleOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order?> UpdateAsync(Order order)
        {
            var ord = await _context.Orders
                .Include(o => o.ChatBox)
                .Include(o => o.Feedbacks)
                .Include(o => o.Reports).SingleOrDefaultAsync(o => o.Id == order.Id);
            if (ord == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            else
            {
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                _context.Entry(order).State = EntityState.Detached;

                return order;
            }
                
        }
    }
}
