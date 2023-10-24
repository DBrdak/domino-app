using MongoDB.Driver;
using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.Domain.Shared;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Infrastructure.Repositories
{
    public sealed class PriceListRepository : IPriceListRepository
    {
        private readonly CatalogContext _context;
        private readonly ProductRepository _productRepository;

        public PriceListRepository(CatalogContext context, ProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        public async Task<List<PriceList>> GetPriceListsAsync(CancellationToken cancellationToken)
        {
            var priceListsCursor = await _context.PriceLists.FindAsync(_ => true, null, cancellationToken);

            var priceLists = await priceListsCursor.ToListAsync(cancellationToken);

            var isRetailPriceListExist = priceLists.Any(pl => pl.Contractor == Contractor.Retail);

            if (!isRetailPriceListExist)
            {
                await AddPriceList(PriceList.CreateRetail("Cennik detaliczny mięsa", Category.Meat), cancellationToken);
                await AddPriceList(PriceList.CreateRetail("Cennik detaliczny wędlin", Category.Sausage), cancellationToken);
                await GetPriceListsAsync(cancellationToken);
            }

            return priceLists;
        }

        public async Task<PriceList?> GetRetailPriceList(Category category, CancellationToken cancellationToken)
        {
            var retailPriceListCursor = await _context.PriceLists.FindAsync(
                pl => pl.Contractor.Name == Contractor.Retail.Name && pl.Category.Value == category.Value,
                null, cancellationToken);

            return await retailPriceListCursor.SingleOrDefaultAsync(cancellationToken);
        }

        public async Task AddPriceList(PriceList priceList, CancellationToken cancellationToken)
        {
            if (ContractorDuplicatesExists(priceList.Contractor, priceList.Category, cancellationToken))
            {
                throw new ApplicationException($"Price list for contractor {priceList.Contractor.Name} with category {priceList.Category.Value} already exists");
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

            //TODO Check if contractor is null

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

            var isValid = PriceListslLineItemDuplicatesNotExists(lineItem.Name, priceList.Contractor, cancellationToken);

            if (!isValid)
            {
                return false;
            }

            priceList.AddLineItem(lineItem);

            var result = await _context.PriceLists.ReplaceOneAsync(
                pl => pl.Id == priceListId,
                priceList, new ReplaceOptions(), cancellationToken);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<PriceList?> AggregateLineItemWithProduct(
            string productId,
            string lineItemName,
            CancellationToken cancellationToken)
        {
            var priceListCategory = await GetPriceListCategory(productId, cancellationToken);

            var priceList = await GetRetailPriceList(priceListCategory, cancellationToken);

            if (priceList is null)
            {
                return null;
            }

            priceList.AggregateLineItemWithProduct(lineItemName, productId);

            var result = await _context.PriceLists.ReplaceOneAsync(
                pl => pl.Id == priceList.Id,
                priceList, new ReplaceOptions(), cancellationToken);

            var isSuccess = result.IsAcknowledged && result.ModifiedCount > 0;

            if (isSuccess)
            {
                return priceList;
            }

            return null;
        }

        public async Task<LineItem?> GetLineItemForProduct(string productId, Category productCategory, CancellationToken cancellationToken, bool isProductInDb = false)
        {
            var product = (await _productRepository.GetProductsAsync("", cancellationToken))
                .SingleOrDefault(p => p.Id == productId);

            if (isProductInDb && product is null)
            {
                throw new ApplicationException($"Product with ID {productId} not found");
            }

            var priceList = await GetRetailPriceList(productCategory, cancellationToken);

            if (priceList is null)
            {
                return null;
            }

            return priceList.LineItems.Single(li => li.ProductId == productId);
        }

        public async Task<bool> SplitLineItemFromProduct(string productId, Category productCategory, CancellationToken cancellationToken)
        {
            var priceList = await GetRetailPriceList(productCategory, cancellationToken);

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

        private bool ContractorDuplicatesExists(Contractor contractor, Category category, CancellationToken cancellationToken) =>
            _context.PriceLists
                .FindAsync(pl => pl.Contractor.Name == contractor.Name && pl.Category.Value == category.Value, null, cancellationToken)
                .Result.ToList(cancellationToken).Any();

        private bool PriceListslLineItemDuplicatesNotExists(string lineItemName, Contractor contractor, CancellationToken cancellationToken) =>
            _context.PriceLists.FindAsync(pl => pl.Contractor.Name == contractor.Name, null, cancellationToken)
                .Result.ToList(cancellationToken)
                .SelectMany(pl => pl.LineItems)
                .All(li => li.Name != lineItemName);

        private async Task<PriceList?> GetPriceList(string priceListId, CancellationToken cancellationToken) =>
            (await GetPriceListsAsync(cancellationToken)).SingleOrDefault(pl => pl.Id == priceListId);

        private async Task<Category> GetPriceListCategory(string productId, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsAsync("", cancellationToken);

            var product = products.SingleOrDefault(p => p.Id == productId);

            return product!.Category;
        }

        //TODO Kontrahent się loguje -> tworzy się cennik na podstawie retailu -> admin edytuje -> cennika nie można usunąć dopóki istnieje kontrahent
    }
}