using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Order.Application.Core;
using OnlineShop.Order.Application.Core.Interfaces;
using OnlineShop.Order.Domain.Entities;

namespace OnlineShop.Order.Application.Features.Commands.CheckoutOrder
{
    public class CheckoutOrderCommand : ICommand<Result<string>>
    {
        public OnlineOrder CheckoutOrder { get; set; }

        public CheckoutOrderCommand()
        {
        }
    }
}