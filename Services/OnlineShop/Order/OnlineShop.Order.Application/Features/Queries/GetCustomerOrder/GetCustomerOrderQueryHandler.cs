using OnlineShop.Order.Application.Abstractions.Messaging;
using OnlineShop.Order.Application.Core;
using OnlineShop.Order.Domain;
using OnlineShop.Order.Domain.OnlineOrders;
using Shared.Domain.Errors;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Order.Application.Features.Queries.GetCustomerOrder
{
    public class GetCustomerOrderQueryHandler : IQueryHandler<GetCustomerOrderQuery, OnlineOrder>
    {
        private readonly IOrderRepository _repository;

        public GetCustomerOrderQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<OnlineOrder>> Handle(GetCustomerOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetCustomerOrders(request.PhoneNumber, request.OrderId);

            if (order is null)
                return Result.Failure<OnlineOrder>(Error.NullValue);

            return Result.Success(order);
        }
    }
}