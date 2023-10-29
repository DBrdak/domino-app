using Shared.Domain.Money;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.AddLineItem
{
    public sealed record AddLineItemRequestValues(string LineItemName, Money NewPrice);
}