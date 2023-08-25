using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Abstractions;

namespace OnlineShop.Order.Domain.OnlineOrders.Events
{
    public sealed record OrderCreatedDomainEvent(string OrderId, string PhoneNumber) : IDomainEvent
    {
    }
}