using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Abstractions.Entities;

namespace OnlineShop.Order.Domain.OnlineOrders.Events
{
    public sealed record OrderDeletedDomainEvent(string ShopId, string OrderId) : IDomainEvent
    {
    }
}
