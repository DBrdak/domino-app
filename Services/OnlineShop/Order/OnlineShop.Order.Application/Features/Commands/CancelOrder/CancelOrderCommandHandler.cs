using OnlineShop.Order.Application.Abstractions.Messaging;
using OnlineShop.Order.Application.Core;
using OnlineShop.Order.Domain.OnlineOrders;
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
            var result = await _repository.CancelOrder(request.Order);

            if (result is false)
                return Result.Failure<bool>(Error.InvalidRequest($"Problem while cancelling order with ID: {request.Order.Id}"));

            return Result.Success(true);
        }
    }
}