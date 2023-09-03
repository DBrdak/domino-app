using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Order.Domain.OnlineOrders
{
    public interface IOrderRepository
    {
        Task<OnlineOrder> GetCustomerOrders(string phoneNumber, string orderId);

        Task<OnlineOrder> CreateOrder(OnlineOrder order);

        Task<bool> CancelOrder(string orderId);
    }
}