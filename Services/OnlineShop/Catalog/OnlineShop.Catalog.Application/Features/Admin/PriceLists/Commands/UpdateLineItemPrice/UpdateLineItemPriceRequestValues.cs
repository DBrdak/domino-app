using Shared.Domain.Money;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.UpdateLineItemPrice;

public sealed record UpdateLineItemPriceRequestValues(string LineItemName, Money NewPrice);