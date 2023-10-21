﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Abstractions.Messaging;

namespace Shops.Application.Features.Commands.DeleteOrderFromShop
{
    public sealed record DeleteOrderFromShopCommand(string ShopId, string OrderId) : ICommand
    {
    }
}