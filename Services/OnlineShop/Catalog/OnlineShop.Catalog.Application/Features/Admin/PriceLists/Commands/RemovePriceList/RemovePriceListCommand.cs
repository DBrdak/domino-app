using Shared.Domain.Abstractions.Messaging;

namespace OnlineShop.Catalog.Application.Features.Admin.PriceLists.Commands.RemovePriceList
{
    public sealed record RemovePriceListCommand(string PriceListId) : ICommand;
}