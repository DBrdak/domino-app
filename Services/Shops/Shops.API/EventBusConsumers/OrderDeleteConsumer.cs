﻿using IntegrationEvents.Domain.Events.OrderDelete;
using MassTransit;
using MediatR;
using Shops.Application.Features.Commands.DeleteOrderFromShop;

namespace Shops.API.EventBusConsumers
{
    public class OrderDeleteConsumer : IConsumer<OrderDeleteEvent>
    {
        private readonly IMediator _mediator;

        public OrderDeleteConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<OrderDeleteEvent> context)
        {
            var command = new DeleteOrderFromShopCommand(context.Message.ShopId, context.Message.OrderId);
            _ = _mediator.Send(command);
        }
    }
}
