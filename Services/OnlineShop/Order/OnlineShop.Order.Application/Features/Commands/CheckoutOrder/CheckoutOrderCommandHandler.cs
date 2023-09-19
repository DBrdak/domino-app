using EventBus.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineShop.Order.Domain.OnlineOrders;

namespace OnlineShop.Order.Application.Features.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, CheckoutResult>
    {
        private readonly IOrderRepository _repository;

        public CheckoutOrderCommandHandler(IOrderRepository repository)
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