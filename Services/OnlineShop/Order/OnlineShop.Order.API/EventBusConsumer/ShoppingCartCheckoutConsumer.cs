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

        public ShoppingCartCheckoutConsumer(IMapper mapper, IMediator mediator, ILogger<ShoppingCartCheckoutConsumer> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ShoppingCartCheckoutEvent> context)
        {
            var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
                _logger.LogError($"{result.Message}");

            _logger.LogInformation($"BasketCheckoutEvent consumed successfully. Created Order Id : {result.Value}");
        }
    }
}