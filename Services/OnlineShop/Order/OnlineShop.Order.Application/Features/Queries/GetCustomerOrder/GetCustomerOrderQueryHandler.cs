using OnlineShop.Order.Application.Contracts;
using OnlineShop.Order.Application.Core;
using OnlineShop.Order.Application.Core.Interfaces;
using OnlineShop.Order.Domain.Entities;

namespace OnlineShop.Order.Application.Features.Queries.GetCustomerOrder
{
    public class GetCustomerOrderQueryHandler : IQueryHandler<GetCustomerOrderQuery, Result<OnlineOrder>>
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
                return Result<OnlineOrder>.Failure("Problem while retriving orders from database");

            return Result<OnlineOrder>.Success(order);
        }
    }
}