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
        private readonly ILogger<ShoppingCartCheckoutConsumer> _logger;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;

        public ShoppingCartCheckoutConsumer(IMapper mapper, IMediator mediator, ILogger<ShoppingCartCheckoutConsumer> logger, IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<ShoppingCartCheckoutEvent> context)
        {
            var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                await _publishEndpoint.Publish(CheckoutResultEvent.Failure(result.Message));
                _logger.LogError($"{result.Message}");
                return;
            }

            await _publishEndpoint.Publish(CheckoutResultEvent.Success(result.Value));
            _logger.LogInformation($"BasketCheckoutEvent consumed successfully. Created Order Id : {result.Value}");
        }
    }
}