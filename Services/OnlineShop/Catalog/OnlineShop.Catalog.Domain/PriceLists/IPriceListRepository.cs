using Microsoft.AspNetCore.Http;
using OnlineShop.Catalog.Domain.Shared;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.PriceLists
{
    public interface IPriceListRepository
    {
        Task<List<PriceList>> GetPriceListsAsync(CancellationToken cancellationToken);

        Task<PriceList?> GetRetailPriceList(Category category, CancellationToken cancellationToken);

        Task AddPriceList(PriceList priceList, CancellationToken cancellationToken);

        Task<bool> RemovePriceList(string priceListId, CancellationToken cancellationToken);

        Task<PriceList?> RemoveLineItem(
            string priceListId,
            string lineItemName,
            CancellationToken cancellationToken);

        Task<PriceList?> UpdateLineItemPrice(
            string priceListId,
            string lineItemName,
            Money newPrice,
            CancellationToken cancellationToken);

        Task<bool> AddLineItem(string priceListId, LineItem lineItem, CancellationToken cancellationToken);

        Task<bool> UploadPriceListFile(string priceListId, IFormFile priceListFile, CancellationToken cancellationToken);

        // Internal use for products

        Task<PriceList?> AggregateLineItemWithProduct(
            string productId,
            string lineItemName,
            CancellationToken cancellationToken);

        Task<bool> SplitLineItemFromProduct(string productId, Category productCategory, CancellationToken cancellationToken);
    }
}