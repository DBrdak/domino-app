using System.Collections.Concurrent;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using OnlineShop.Order.Application.Features.Commands.CheckoutOrder;

namespace OnlineShop.Order.API.EventBusConsumer
{
    public class ShoppingCartCheckoutConsumer : IConsumer<ShoppingCartCheckoutEvent>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ShoppingCartCheckoutConsumer(IMapper mapper, IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<ShoppingCartCheckoutEvent> context)
        {
            var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                await context.RespondAsync(CheckoutResultResponse.Success(result.Value));
            else
                await context.RespondAsync(CheckoutResultResponse.Failure(result.Message));
        }
    }
}