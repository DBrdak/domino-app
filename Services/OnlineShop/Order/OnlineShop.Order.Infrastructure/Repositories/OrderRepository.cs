using Microsoft.EntityFrameworkCore;
using OnlineShop.Order.Application.Contracts;
using OnlineShop.Order.Domain.Entities;
using OnlineShop.Order.Infrastructure.Persistence;

namespace OnlineShop.Order.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public async Task<OnlineOrder> GetCustomerOrders(string phoneNumber, string orderId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.PhoneNumber == phoneNumber
                                          && o.OrderId == orderId);
        }

        public async Task<OnlineOrder> CreateOrder(OnlineOrder order)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CancelOrder(OnlineOrder order)
        {
            order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == order.OrderId);

            if (order is null)
                return false;

            order.IsCanceled = true;
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}