using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Abstractions.Messaging;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.GetPriceLists
{
    public sealed record GetPriceListsQuery() : IQuery<List<PriceList>>;
}