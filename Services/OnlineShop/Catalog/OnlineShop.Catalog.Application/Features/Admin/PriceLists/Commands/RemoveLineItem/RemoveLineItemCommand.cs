using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Abstractions.Messaging;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.RemoveLineItem
{
    public sealed record RemoveLineItemCommand(
        string PriceListId,
        string LineItemName) : ICommand<PriceList>;
}