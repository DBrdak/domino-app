using IntegrationEvents.Domain.Events.OrderCreate;
using IntegrationEvents.Domain.Results;
using MassTransit;
using MediatR;
using Shared.Domain.Exceptions;
using Shops.Application.Features.Commands.AggregateOrderWithShop;
using Shops.Application.Features.Queries.GetShopByDeliveryInfo;

namespace Shops.API.EventBusConsumers
{
    public class OrderCreateConsumer : IConsumer<OrderCreateEvent>
    {
        private readonly IMediator _mediator;

        public OrderCreateConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<OrderCreateEvent> context)
        {
            var query = new GetShopIdByDeliveryInfoQuery(
                context.Message.DeliveryLocation,
                context.Message.DeliveryDate);

            var queryResult = await _mediator.Send(query);

            var response = new CheckoutShopResult(queryResult.Value, queryResult.Error.Name, queryResult.IsSuccess);

            if (!response.IsSuccess && response.Error is not null)
            {
                throw new IntegrationEventException<CheckoutShopResult>(response.Error);
            }

            var command = new AggregateOrderWithShopCommand(context.Message.OrderId, queryResult.Value);
            await _mediator.Send(command);

            await context.RespondAsync(response);
        }
    }
}
