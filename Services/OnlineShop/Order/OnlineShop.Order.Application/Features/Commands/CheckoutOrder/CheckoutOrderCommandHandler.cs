using EventBus.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineShop.Order.Application.Abstractions.Messaging;
using OnlineShop.Order.Application.Core;
using OnlineShop.Order.Domain.OnlineOrders;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Order.Application.Features.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, CheckoutResult>
    {
        private readonly IOrderRepository _repository;

        public CheckoutOrderCommandHandler(IOrderRepository repository, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _repository = repository;
        }

        public async Task<CheckoutResult> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var newOrder = await _repository.CreateOrder(request.CheckoutOrder);

            if (newOrder is null)
                return CheckoutResult.Failure("Problem while checking out shopping cart");

            return CheckoutResult.Success(newOrder.Id);
        }
    }
}