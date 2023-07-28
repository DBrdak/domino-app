using OnlineShop.Order.Application.Contracts;
using OnlineShop.Order.Application.Core;
using OnlineShop.Order.Application.Core.Interfaces;

namespace OnlineShop.Order.Application.Features.Commands.CancelOrder
{
    public class CancelOrderCommandHandler : ICommandHandler<CancelOrderCommand, Result<bool>>
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
                return Result<bool>.Failure($"Problem while cancelling order with ID: {request.Order.OrderId}");

            return Result<bool>.Success(true);
        }
    }
}