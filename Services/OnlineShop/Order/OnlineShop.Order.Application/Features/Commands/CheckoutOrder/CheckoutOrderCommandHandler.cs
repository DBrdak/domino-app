using IntegrationEvents.Domain.Results;
using MediatR;
using OnlineShop.Order.Domain.OnlineOrders;

namespace OnlineShop.Order.Application.Features.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, CheckoutOrderResult>
    {
        private readonly IOrderRepository _repository;

        public CheckoutOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<CheckoutOrderResult> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var newOrder = await _repository.CreateOrder(request.CheckoutOrder);

            if (newOrder is null)
                return CheckoutOrderResult.Failure("Błąd podczas składania zamówienia");

            return CheckoutOrderResult.Success(newOrder.Id);
        }
    }
}