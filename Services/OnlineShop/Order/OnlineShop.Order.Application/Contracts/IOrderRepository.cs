using OnlineShop.Order.Domain.Entities;

namespace OnlineShop.Order.Application.Contracts
{
    public interface IOrderRepository
    {
        Task<OnlineOrder> GetCustomerOrders(string phoneNumber, string orderId);

        Task<OnlineOrder> CreateOrder(OnlineOrder order);

        Task<bool> CancelOrder(OnlineOrder order);
    }
}