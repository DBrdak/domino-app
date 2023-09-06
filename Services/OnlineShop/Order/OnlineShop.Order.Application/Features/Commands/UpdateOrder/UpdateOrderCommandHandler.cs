using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Order.Application.Abstractions.Messaging;
using OnlineShop.Order.Domain.OnlineOrders;
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
            var updateTask = _orderRepository.UpdateOrder(request.Status);

            if (request.SmsMessage is not null)
            {
                // TODO Send sms
            }

            await Task.WhenAll(updateTask);

            return Result.Success();
        }
    }
}