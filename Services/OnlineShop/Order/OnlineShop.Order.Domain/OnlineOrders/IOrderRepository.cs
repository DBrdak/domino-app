namespace OnlineShop.Order.Domain.OnlineOrders
{
    public interface IOrderRepository
    {
        Task<OnlineOrder?> GetCustomerOrders(string phoneNumber, string orderId);

        Task<OnlineOrder> CreateOrder(OnlineOrder order);

        Task<bool> CancelOrder(string orderId);

        Task<List<OnlineOrder>> GetAllOrders(CancellationToken cancellationToken);

        Task<bool> UpdateOrder(string orderId, string orderStatus, CancellationToken cancellationToken, OnlineOrder? modifiedOrder = null);

        Task DeleteAggregatedOrderIdFromShop(string orderId, string shopId);
    }
}