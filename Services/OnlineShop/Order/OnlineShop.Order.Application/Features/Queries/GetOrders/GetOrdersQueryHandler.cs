using OnlineShop.Order.Domain.OnlineOrders;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Order.Application.Features.Queries.GetOrders
{
    internal sealed class GetOrdersQueryHandler : ICommandHandler<GetOrdersQuery, List<OnlineOrder>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result<List<OnlineOrder>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetAllOrders(cancellationToken);
        }
    }
}