using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Order.Application.Abstractions.Messaging;
using OnlineShop.Order.Domain.OnlineOrders;

namespace OnlineShop.Order.Application.Features.Queries.GetOrders
{
    public sealed record GetOrdersQuery() : ICommand<List<OnlineOrder>>;
}