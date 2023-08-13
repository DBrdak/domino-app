using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OnlineShop.Order.Application.Contracts;
using OnlineShop.Order.Application.Core;
using OnlineShop.Order.Application.Core.Interfaces;

namespace OnlineShop.Order.Application.Features.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : ICommandHandler<CheckoutOrderCommand, Result<string>>
    {
        private readonly IOrderRepository _repository;

        public CheckoutOrderCommandHandler(IOrderRepository repository, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var newOrder = await _repository.CreateOrder(request.CheckoutOrder);

            if (newOrder is null)
                return Result<string>.Failure($"Problem while creating order");

            return Result<string>.Success(newOrder.OrderId);
        }
    }
}