using OnlineShop.Order.Domain.OnlineOrders;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Order.Application.Features.Commands.CancelOrder
{
    public class CancelOrderCommandHandler : ICommandHandler<CancelOrderCommand, bool>
    {
        private readonly IOrderRepository _repository;

        public CancelOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<bool>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.CancelOrder(request.OrderId);

            if (result is false)
                return Result.Failure<bool>(Error.InvalidRequest($"Anulowanie nie powiodło się, spróbuj ponownie później"));

            return Result.Success(true);
        }
    }
}