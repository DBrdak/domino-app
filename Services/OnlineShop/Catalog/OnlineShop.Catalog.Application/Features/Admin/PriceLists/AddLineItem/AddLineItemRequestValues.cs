using Shared.Domain.Money;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.AddLineItem
{
    public sealed record AddLineItemRequestValues(string LineItemName, Money NewPrice);
}