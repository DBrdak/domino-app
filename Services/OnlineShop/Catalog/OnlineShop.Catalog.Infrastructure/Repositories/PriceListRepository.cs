using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Money;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Infrastructure.Repositories
{
    public sealed class PriceListRepository : IPriceListRepository
    {
        private readonly CatalogContext _context;

        public PriceListRepository(CatalogContext context)
        {
            _context = context;
        }

        public async Task<List<PriceList>> GetPriceListsAsync(CancellationToken cancellationToken)
        {
            var priceListsCursor = await _context.PriceLists.FindAsync(_ => true);

            return await priceListsCursor.ToListAsync(cancellationToken);
        }

        public async Task<PriceList> GetRetailPriceList(CancellationToken cancellationToken)
        {
            var retailPriceListCursor = await _context.PriceLists.FindAsync(
                pl => pl.Contractor == Contractor.Retail);

            return await retailPriceListCursor.SingleOrDefaultAsync(cancellationToken);
        }

        public async Task AddPriceList(PriceList priceList, CancellationToken cancellationToken)
        {
            if (priceList.Contractor == Contractor.Retail &&
                CheckForRetailDuplicates(cancellationToken))
            {
                throw new ApplicationException("Retail price list already exists");
            }

            await _context.PriceLists.InsertOneAsync(priceList, null, cancellationToken);
        }

        private bool CheckForRetailDuplicates(CancellationToken cancellationToken) =>
            _context.PriceLists
                .FindAsync(pl => pl.Contractor == Contractor.Retail, null, cancellationToken)
                .Result.ToList().Any();

        public async Task<bool> RemovePriceList(PriceList priceList, CancellationToken cancellationToken)
        {
            if (priceList.Contractor == Contractor.Retail)
            {
                throw new ApplicationException("Retail price list cannot be deleted");
            }

            //TODO Sprawdzam czy kontrahent jest null

            var result = await _context.PriceLists.DeleteOneAsync(pl => pl.Id == priceList.Id, null, cancellationToken);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<bool> RemoveLineItem(string priceListId, string lineItemName, CancellationToken cancellationToken)
        {
            //TODO Sprawdzam czy istnieje powiązany produkt

            var update = Builders<PriceList>.Update.PullFilter(pl => pl.LineItems, li => li.Name == lineItemName);

            var result = await _context.PriceLists.UpdateOneAsync(
                            pl => pl.Id == priceListId, update, null, cancellationToken);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> UpdateLineItemPrice(string priceListId, string lineItemName, Money newPrice, CancellationToken cancellationToken)
        {
            var update = Builders<PriceList>.Update
                .Set(pl => pl.LineItems.Single(li => li.Name == lineItemName).Price, newPrice);

            var result = await _context.PriceLists.UpdateOneAsync(
                            pl => pl.Id == priceListId, update, null, cancellationToken);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> AddLineItem(string priceListId, LineItem lineItem, CancellationToken cancellationToken)
        {
            var update = Builders<PriceList>.Update
                .Push(pl => pl.LineItems, lineItem);

            var result = await _context.PriceLists.UpdateOneAsync(
                pl => pl.Id == priceListId, update, null, cancellationToken);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task AggregateLineItemWithProduct(
            string productId,
            string lineItemName,
            CancellationToken cancellationToken)
        {
            var priceList = await GetRetailPriceList(cancellationToken);
            var product = (await _context.Products.FindAsync(p => p.Id == productId)).SingleOrDefault(cancellationToken);

            if (priceList is null)
            {
                throw new ApplicationException("Retail price list not found");
            }

            if (product is null)
            {
                throw new ApplicationException($"Product with ID {productId} not found");
            }

            var update = Builders<PriceList>.Update
                .Set(pl => pl.LineItems.Single(li => li.Name == lineItemName).ProductId, productId);

            var result = await _context.PriceLists.UpdateOneAsync(
                pl => pl.Id == priceList.Id, update, null, cancellationToken);
        }

        public async Task<LineItem> GetLineItemForProduct(string productId, CancellationToken cancellationToken)
        {
            var product = (await _context.Products.FindAsync(p => p.Id == productId)).SingleOrDefault(cancellationToken);

            if (product is null)
            {
                throw new ApplicationException($"Product with ID {productId} not found");
            }

            var priceList = await GetRetailPriceList(cancellationToken);

            if (priceList is null)
            {
                throw new ApplicationException("Retail price list not found");
            }

            return priceList.LineItems.Single(li => li.ProductId == productId);
        }

        //TODO Kontrahent się loguje -> tworzy się cennik na podstawie retailu -> admin edytuje -> cennika nie można usunąć dopóki istnieje kontrahent
    }
}