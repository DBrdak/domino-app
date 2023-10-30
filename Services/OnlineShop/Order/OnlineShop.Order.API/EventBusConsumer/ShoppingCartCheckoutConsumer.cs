using IntegrationEvents.Domain.Events.ShoppingCartCheckout;
using IntegrationEvents.Domain.Results;
using MassTransit;
using MediatR;
using OnlineShop.Order.Application.Features.Commands.CheckoutOrder;
using OnlineShop.Order.Domain.OnlineOrders;
using Shared.Domain.Exceptions;

namespace OnlineShop.Order.API.EventBusConsumer
{
    public class ShoppingCartCheckoutConsumer : IConsumer<ShoppingCartCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ShoppingCartCheckoutConsumer> _logger;

        public ShoppingCartCheckoutConsumer(IMediator mediator, ILogger<ShoppingCartCheckoutConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ShoppingCartCheckoutEvent> context)
        {
            _logger.LogInformation($"Order service received shopping cart with ID: {context.Message.ShoppingCartId}");

            var command = new CheckoutOrderCommand(OnlineOrder.Create(context.Message));
            var result = await _mediator.Send(command);

            if (!result.IsSuccess && result.Error is not null)
            {
                _logger.LogError($"Failure during shopping cart checkout for ID: {context.Message.ShoppingCartId}");
                throw new IntegrationEventException<CheckoutOrderResult>(result.Error);
            }

            _logger.LogInformation($"Shopping cart with ID: {context.Message.ShoppingCartId} was successfully checkouted");

            await context.RespondAsync(result);
        }
    }
}