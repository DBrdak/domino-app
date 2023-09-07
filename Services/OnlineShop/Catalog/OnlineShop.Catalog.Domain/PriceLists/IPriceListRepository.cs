using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Catalog.Domain.PriceLists
{
    public interface IPriceListRepository
    {
        Task<List<PriceList>> GetPriceListsAsync();

        Task<PriceList> GetRetailPriceList();

        Task AddPriceList(PriceList priceList);

        Task RemovePriceList(PriceList priceList);

        Task RemoveLineItem(string priceListId, string lineItemName);

        Task UpdateLineItemPrice(string priceListId, string lineItemName);

        Task AddLineItem(string priceListId, LineItem lineItem);

        Task<LineItem> GetLineItemForProduct(string productId);

        Task AggregateLineItemWithProduct(string productId, string lineItemName);
    }
}