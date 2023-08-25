using Microsoft.EntityFrameworkCore;
using OnlineShop.Order.Application.Contracts;
using OnlineShop.Order.Domain;
using OnlineShop.Order.Domain.OnlineOrders;
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
            return await _context.Set<OnlineOrder>()
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.PhoneNumber == phoneNumber
                                          && o.Id == orderId);
        }

        public async Task<OnlineOrder> CreateOrder(OnlineOrder order)
        {
            await _context.Set<OnlineOrder>().AddAsync(order);
            var isSuccess = await _context.SaveChangesAsync() > 0;

            if (!isSuccess)
                return null;

            return order;
        }

        public async Task<bool> CancelOrder(OnlineOrder order)
        {
            return false;
        }
    }
}