using IntegrationEvents.Domain.Events.OrderCreate;
using IntegrationEvents.Domain.Events.OrderDelete;
using IntegrationEvents.Domain.Events.OrderShopQuery;
using IntegrationEvents.Domain.Results;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Order.Domain.OnlineOrders;
using OnlineShop.Order.Domain.OrderItems;
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

        public async Task<List<OnlineOrder>> PrepareOrdersForPrint(IEnumerable<string> ordersId, CancellationToken cancellationToken)
        {
            var orders = await _context.Set<OnlineOrder>()
                .Include(o => o.Items)
                .Where(o => ordersId.Contains(o.Id))
                .OrderBy(o => o.DeliveryDate.Start)
                .ToListAsync(cancellationToken);

            orders.ForEach(o => o.Print());

            var result = await _context.SaveChangesAsync() > 0;

            return result ? orders : new ();
        }

        public async Task CatchPrintingError(IEnumerable<string> ordersId, CancellationToken cancellationToken)
        {
            var orders = await _context.Set<OnlineOrder>()
                .Include(o => o.Items)
                .Where(o => ordersId.Contains(o.Id))
                .OrderBy(o => o.DeliveryDate.Start)
                .ToListAsync(cancellationToken);

            orders.ToList().ForEach(o => o.PrintLost());

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> UpdateOrder(
            string orderId,
            string? orderStatus,
            CancellationToken cancellationToken,
            string? smsMessage = null,
            IEnumerable<OrderItem>? modifiedOrderItems = null,
            bool? isPrinted = null)
        {
            var order = await _context.Set<OnlineOrder>()
                .Include(onlineOrder => onlineOrder.Items)
                .SingleOrDefaultAsync(o => o.Id == orderId, cancellationToken);

            if (order is null)
            {
                return false;
            }

            if(orderStatus is not null)
            {
                order.UpdateStatus(orderStatus, modifiedOrderItems);
            }

            if (!string.IsNullOrWhiteSpace(smsMessage))
            {
                // TODO Send SMS
            }

            if (isPrinted is false)
            {
                order.PrintLost();
            }

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result > 0;
        }

        public async Task DeleteAggregatedOrderIdFromShop(string orderId, string shopId)
        {
            _ = _bus.Publish<OrderDeleteEvent>(new(shopId, orderId));
        }

        public async Task<OrderShopQueryResult?> GetShopsName(IEnumerable<string> shopsId, CancellationToken cancellationToken)
        {
            var eventMessage = new OrderShopQueryEvent(shopsId);
            var client = _bus.CreateRequestClient<OrderShopQueryEvent>();
            var response = await client.GetResponse<OrderShopQueryResult>(eventMessage, cancellationToken);

            if (!response.Message.ShopNameWithId.Any())
            {
                return null;
            }
            
            return response.Message;
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