using EventBus.Domain.Results;

namespace OnlineShop.Order.Domain.OnlineOrders
{
    public interface IOrderRepository
    {
        Task<OnlineOrder?> GetCustomerOrders(string phoneNumber, string orderId);

        Task<OnlineOrder> CreateOrder(OnlineOrder order);

        Task<bool> CancelOrder(string orderId);

        Task<List<OnlineOrder>> GetAllOrders(CancellationToken cancellationToken);
        Task<List<OnlineOrder>> PrepareOrdersForPrint(IEnumerable<string> ordersId, CancellationToken cancellationToken);
        Task CatchPrintingError(IEnumerable<string> ordersId, CancellationToken cancellationToken);

        Task<bool> UpdateOrder(string orderId, string? orderStatus, CancellationToken cancellationToken, string? smsMessage = null, OnlineOrder? modifiedOrder = null, bool? isPrinted = null);

        Task DeleteAggregatedOrderIdFromShop(string orderId, string shopId);

        Task<OrderShopQueryResult?> GetShopsName(IEnumerable<string> shopsId, CancellationToken cancellationToken);
    }
}