using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Order.Application.Abstractions.Messaging;

namespace OnlineShop.Order.Application.Features.Commands.UpdateOrder
{
    public sealed record UpdateOrderCommand(string Status, string? SmsMessage) : ICommand;
}