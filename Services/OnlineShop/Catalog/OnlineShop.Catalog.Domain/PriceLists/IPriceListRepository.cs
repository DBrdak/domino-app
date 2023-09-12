using Shared.Domain.Money;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Catalog.Domain.PriceLists
{
    public interface IPriceListRepository
    {
        Task<List<PriceList>> GetPriceListsAsync(CancellationToken cancellationToken);

        Task<PriceList?> GetRetailPriceList(CancellationToken cancellationToken);

        Task AddPriceList(PriceList priceList, CancellationToken cancellationToken);

        Task<bool> RemovePriceList(string priceListId, CancellationToken cancellationToken);

        Task<bool> RemoveLineItem(
            string priceListId,
            string lineItemName,
            CancellationToken cancellationToken);

        Task<bool> UpdateLineItemPrice(
            string priceListId,
            string lineItemName,
            Money newPrice,
            CancellationToken cancellationToken);

        Task<bool> AddLineItem(string priceListId, LineItem lineItem, CancellationToken cancellationToken);

        // Internal use for products

        Task<bool> AggregateLineItemWithProduct(
            string productId,
            string lineItemName,
            CancellationToken cancellationToken);

        Task<LineItem?> GetLineItemForProduct(string productId, CancellationToken cancellationToken, bool isProductInDb = false);

        Task<bool> SplitLineItemFromProduct(string productId, CancellationToken cancellationToken);
    }
}