using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Order.Domain.OnlineOrders;
using OnlineShop.Order.Domain.OnlineOrders.Events;
using Shared.Domain.Abstractions.Entities;

namespace OnlineShop.Order.Application.Features.DomainEventHandlers.OrderDeleted
{
    internal class OrderDeletedDomainEventHandler : IDomainEventHandler<OrderDeletedDomainEvent>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderDeletedDomainEventHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(OrderDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            await _orderRepository.DeleteAggregatedOrderIdFromShop(notification.OrderId, notification.ShopId);
        }
    }
}
