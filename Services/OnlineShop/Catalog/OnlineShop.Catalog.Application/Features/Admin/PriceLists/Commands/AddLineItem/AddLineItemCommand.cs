using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.AddLineItem
{
    public sealed record AddLineItemCommand(
        string PriceListId,
        string Name,
        Money Price) : ICommand;
}