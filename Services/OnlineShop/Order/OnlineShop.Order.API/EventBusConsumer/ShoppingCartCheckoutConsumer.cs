using EventBus.Domain.Events;
using MassTransit;
using MediatR;
using OnlineShop.Order.Application.Features.Commands.CheckoutOrder;
using OnlineShop.Order.Domain.OnlineOrders;

namespace OnlineShop.Order.API.EventBusConsumer
{
    public class ShoppingCartCheckoutConsumer : IConsumer<ShoppingCartCheckoutEvent>
    {
        private readonly IMediator _mediator;

        public ShoppingCartCheckoutConsumer(IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<ShoppingCartCheckoutEvent> context)
        {
            var command = new CheckoutOrderCommand(OnlineOrder.Create(context.Message));
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                await context.RespondAsync(result);
            else
                throw new ApplicationException(result.Error);
        }
    }
}