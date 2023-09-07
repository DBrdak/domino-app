using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Catalog.Domain.PriceLists;

namespace OnlineShop.Catalog.Infrastructure.Repositories
{
    public sealed class PriceListRepository : IPriceListRepository
    {
        private readonly CatalogContext _context;

        public PriceListRepository(CatalogContext context)
        {
            _context = context;
        }

        public async Task<List<PriceList>> GetPriceListsAsync()
        {
            return null;
        }

        public async Task<PriceList> GetRetailPriceList()
        {
            return null;
        }

        public async Task AddPriceList(PriceList priceList)
        {
        }

        public async Task RemovePriceList(PriceList priceList)
        {
        }

        public async Task RemoveLineItem(string priceListId, string lineItemName)
        {
        }

        public async Task UpdateLineItemPrice(string priceListId, string lineItemName)
        {
        }

        public async Task AddLineItem(string priceListId, LineItem lineItem)
        {
        }

        public async Task<LineItem> GetLineItemForProduct(string productId)
        {
            return null;
        }

        public async Task AggregateLineItemWithProduct(string productId, string lineItemName)
        {
        }
    }
}