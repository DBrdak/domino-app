using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.Domain.Products;
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

        public async Task<PriceList?> GetRetailPriceList(CancellationToken cancellationToken)
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
                throw new ApplicationException($"Price list for contractor {priceList.Contractor.Name} already exists");
            }

            await _context.PriceLists.InsertOneAsync(priceList, null, cancellationToken);
        }

        public async Task<bool> RemovePriceList(string priceListId, CancellationToken cancellationToken)
        {
            var priceList = await GetPriceList(priceListId, cancellationToken);

            if (priceList is null)
            {
                return false;
            }

            if (priceList.Contractor == Contractor.Retail)
            {
                throw new ApplicationException("Retail price list cannot be deleted");
            }

            //TODO Sprawdzam czy kontrahent jest null

            var result = await _context.PriceLists.DeleteOneAsync(pl => pl.Id == priceList.Id, null, cancellationToken);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<PriceList?> RemoveLineItem(string priceListId, string lineItemName, CancellationToken cancellationToken)
        {
            var priceList = await GetPriceList(priceListId, cancellationToken);

            if (priceList is null)
            {
                return null;
            }

            priceList.DeleteLineItem(lineItemName);

            var result = await _context.PriceLists.ReplaceOneAsync(
                pl => pl.Id == priceListId,
                priceList, new ReplaceOptions(), cancellationToken);

            if (!result.IsAcknowledged && !(result.ModifiedCount > 0))
            {
                return null;
            }

            return priceList;
        }

        public async Task<PriceList?> UpdateLineItemPrice(string priceListId, string lineItemName, Money newPrice, CancellationToken cancellationToken)
        {
            var priceList = await GetPriceList(priceListId, cancellationToken);

            if (priceList is null)
            {
                return null;
            }

            priceList.UpdateLineItemPrice(lineItemName, newPrice);

            var result = await _context.PriceLists.ReplaceOneAsync(
                pl => pl.Id == priceListId,
                priceList, new ReplaceOptions(), cancellationToken);

            if (!result.IsAcknowledged && !(result.ModifiedCount > 0))
            {
                return null;
            }

            return priceList;
        }

        public async Task<bool> AddLineItem(string priceListId, LineItem lineItem, CancellationToken cancellationToken)
        {
            var priceList = await GetPriceList(priceListId, cancellationToken);

            if (priceList == null)
            {
                return false;
            }

            priceList.AddLineItem(lineItem);

            var result = await _context.PriceLists.ReplaceOneAsync(
                pl => pl.Id == priceListId,
                priceList, new ReplaceOptions(), cancellationToken);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> AggregateLineItemWithProduct(
            string productId,
            string lineItemName,
            CancellationToken cancellationToken)
        {
            var priceList = await GetRetailPriceList(cancellationToken);

            if (priceList is null)
            {
                return false;
            }

            priceList.AggregateLineItemWithProduct(lineItemName, productId);

            var result = await _context.PriceLists.ReplaceOneAsync(
                pl => pl.Id == priceList.Id,
                priceList, new ReplaceOptions(), cancellationToken);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<LineItem?> GetLineItemForProduct(string productId, CancellationToken cancellationToken, bool isProductInDb = false)
        {
            var product = (await _context.Products.FindAsync(p => p.Id == productId, null, cancellationToken)).SingleOrDefault();

            if (isProductInDb && product is null)
            {
                throw new ApplicationException($"Product with ID {productId} not found");
            }

            var priceList = await GetRetailPriceList(cancellationToken);

            if (priceList is null)
            {
                return null;
            }

            return priceList.LineItems.Single(li => li.ProductId == productId);
        }

        public async Task<bool> SplitLineItemFromProduct(string productId, CancellationToken cancellationToken)
        {
            var priceList = await GetRetailPriceList(cancellationToken);

            if (priceList is null)
            {
                return false;
            }

            var isExist = priceList.LineItems.SingleOrDefault(li => li.ProductId == productId) is not null;

            if (!isExist)
            {
                return true;
            }

            priceList.SplitLineItemFromProduct(productId);

            var result = await _context.PriceLists.ReplaceOneAsync(
                pl => pl.Id == priceList.Id,
                priceList, new ReplaceOptions(), cancellationToken);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        private bool CheckForRetailDuplicates(CancellationToken cancellationToken) =>
            _context.PriceLists
                .FindAsync(pl => pl.Contractor.Name == Contractor.Retail.Name, null, cancellationToken)
                .Result.ToList().Any();

        private async Task<PriceList?> GetPriceList(string priceListId, CancellationToken cancellationToken) =>
            GetPriceListsAsync(cancellationToken).Result.SingleOrDefault(pl => pl.Id == priceListId);

        //TODO Kontrahent się loguje -> tworzy się cennik na podstawie retailu -> admin edytuje -> cennika nie można usunąć dopóki istnieje kontrahent
    }
}