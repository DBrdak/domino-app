using Microsoft.EntityFrameworkCore;
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
                .AsNoTracking()
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

        public async Task<bool> CancelOrder(string orderId)
        {
            var order = await _context.Set<OnlineOrder>()
                .SingleOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return false;

            order.Cancel();
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<List<OnlineOrder>> GetAllOrders(CancellationToken cancellationToken)
        {
            return await _context.Set<OnlineOrder>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> UpdateOrder(
            string orderId,
            string orderStatus,
            CancellationToken cancellationToken,
            OnlineOrder? modifiedOrder = null)
        {
            var order = await _context.Set<OnlineOrder>()
                .SingleOrDefaultAsync(o => o.Id == orderId, cancellationToken);

            if (order is null)
            {
                return false;
            }

            order.UpdateStatus(orderStatus, modifiedOrder);
            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0;
        }
    }
}