using OnlineShop.Order.Domain.OnlineOrders;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Order.Application.Features.Commands.UpdateOrder
{
    internal class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var updateTask = _orderRepository.UpdateOrder(
                request.OrderId,
                request.Status,
                cancellationToken,
                request.ModifiedOrder);

            if (request.SmsMessage is not null)
            {
                // TODO Send sms
            }

            await Task.WhenAny(updateTask);

            return Result.Success();
        }
    }
}