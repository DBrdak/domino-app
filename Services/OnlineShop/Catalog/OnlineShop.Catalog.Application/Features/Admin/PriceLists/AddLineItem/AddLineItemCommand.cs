using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Application.Abstractions.Messaging;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.AddLineItem
{
    public sealed record AddLineItemCommand(
        string PriceListId,
        string Name,
        Money Price) : ICommand;
}