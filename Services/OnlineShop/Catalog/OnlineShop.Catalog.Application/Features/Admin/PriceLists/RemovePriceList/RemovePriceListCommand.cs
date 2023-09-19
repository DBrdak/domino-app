using Shared.Domain.Abstractions.Messaging;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.RemovePriceList
{
    public sealed record RemovePriceListCommand(string PriceListId) : ICommand;
}