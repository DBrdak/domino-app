using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.PriceLists
{
    public interface IPriceListRepository
    {
        Task<List<PriceList>> GetPriceListsAsync(CancellationToken cancellationToken);

        Task<PriceList?> GetRetailPriceList(PriceListCategory category, CancellationToken cancellationToken);

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

        // Internal use for products

        Task<PriceList?> AggregateLineItemWithProduct(
            string productId,
            string lineItemName,
            CancellationToken cancellationToken);

        Task<LineItem?> GetLineItemForProduct(string productId, Category productCategory, CancellationToken cancellationToken, bool isProductInDb = false);

        Task<bool> SplitLineItemFromProduct(string productId, Category productCategory, CancellationToken cancellationToken);
    }
}