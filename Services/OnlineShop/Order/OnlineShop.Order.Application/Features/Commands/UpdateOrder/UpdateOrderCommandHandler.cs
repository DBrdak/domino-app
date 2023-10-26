﻿using OnlineShop.Order.Domain.OnlineOrders;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Errors;
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
            var updateResult = await _orderRepository.UpdateOrder(
                request.OrderId,
                request.Status,
                cancellationToken,
                request.SmsMessage,
                request.ModifiedOrder,
                request.IsPrinted);

            if (!updateResult)
            {
                return Result.Failure(Error.TaskFailed($"Error during updating order with ID: {request.OrderId}"));
            }

            return Result.Success();
        }
    }
}