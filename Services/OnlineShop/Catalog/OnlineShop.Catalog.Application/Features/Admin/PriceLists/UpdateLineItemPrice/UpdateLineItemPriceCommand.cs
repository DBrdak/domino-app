using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.UpdateLineItemPrice
{
    public sealed record UpdateLineItemPriceCommand(
        string LineItemName,
        Money NewPrice,
        string PriceListId) : ICommand<PriceList>;
}