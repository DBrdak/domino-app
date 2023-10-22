using EventBus.Domain.Events;
using EventBus.Domain.Events.OrderCreate;
using EventBus.Domain.Events.OrderDelete;
using EventBus.Domain.Results;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Order.Domain.OnlineOrders;
using OnlineShop.Order.Infrastructure.Persistence;

namespace OnlineShop.Order.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;
        private readonly IBus _bus;

        public OrderRepository(OrderContext context, IBus bus)
        {
            _context = context;
            _bus = bus;
        }

        public async Task<OnlineOrder?> GetCustomerOrders(string phoneNumber, string orderId)
        {
            return await _context.Set<OnlineOrder>()
                .Include(o => o.Items)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.PhoneNumber == phoneNumber
                                          && o.Id == orderId);
        }

        public async Task<OnlineOrder?> CreateOrder(OnlineOrder order)
        {
            var shopId = await GetShopIdForOrder(order);
            order.SetShopId(shopId);
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
                .Include(o => o.Items)
                .AsNoTracking()
                .OrderBy(o => o.DeliveryDate.Start)
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

        public async Task DeleteAggregatedOrderIdFromShop(string orderId, string shopId)
        {
            _ = _bus.Publish<OrderDeleteEvent>(new(shopId, orderId));
        }

        private async Task<string> GetShopIdForOrder(OnlineOrder order)
        {
            var eventMessage = new OrderCreateEvent(order.DeliveryLocation, order.DeliveryDate, order.Id);
            var client = _bus.CreateRequestClient<OrderCreateEvent>();
            var response = await client.GetResponse<CheckoutShopResult>(eventMessage);

            if (!response.Message.IsSuccess)
            {
                throw new ApplicationException(response.Message.Error);
            }

            return response.Message.ShopId;
        }
    }
}