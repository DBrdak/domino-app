using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.Photo;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Infrastructure.Repositories
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;
        private readonly IPhotoRepository _photoRepository;
        private readonly IPriceListRepository _priceListRepository;

        public ProductRepository(CatalogContext context, IPhotoRepository photoRepository, IPriceListRepository priceListRepository)
        {
            _context = context;
            _photoRepository = photoRepository;
            _priceListRepository = priceListRepository;
        }

        public async Task<PagedList<Product>> GetProductsAsync
            (int page, string sortOrder, string sortBy, int pageSize, string category, string name,
                decimal? minPrice, decimal? maxPrice, bool? isAvailable, bool? isDiscounted, CancellationToken cancellationToken = default)
        {
            var filter = SearchEngine.ApplyFiltering(category, minPrice, maxPrice, isAvailable, isDiscounted);

            var options = SearchEngine.ApplySorting(sortOrder, sortBy);

            var products = await (await _context.Products.FindAsync(filter, options, cancellationToken)).ToListAsync(cancellationToken);

            products = SearchEngine.ApplySearch(name, products);

            return PagedList<Product>.Create(products, page, pageSize);
        }

        public async Task<List<Product>> GetProductsAsync(string searchPhrase, CancellationToken cancellationToken = default)
        {
            var products = await _context.Products.Find(_ => true).ToListAsync(cancellationToken);
            products = SearchEngine.ApplySearch(searchPhrase, products);

            return products;
        }

        public async Task<Product?> UpdateProduct(UpdateValues newValues, CancellationToken cancellationToken = default)
        {
            var product = (await _context.Products.FindAsync(
                Builders<Product>.Filter.Eq(p => p.Id, newValues.Id), null,
                cancellationToken)).FirstOrDefault(cancellationToken);

            if (product is null)
            {
                return null;
            }

            if (newValues.ImageUrl != product.Image.Url)
            {
                await _photoRepository.DeletePhoto(product.Image.Url);
            }

            product.Update(newValues);

            var result = await _context.Products.ReplaceOneAsync(
                Builders<Product>.Filter.Eq(p => p.Id, product.Id),
                product, new ReplaceOptions(), cancellationToken);

            return result.IsAcknowledged && result.MatchedCount > 0 ? product : null;
        }

        public async Task<Product?> Add(CreateValues values, IFormFile photoFile, CancellationToken cancellationToken = default)
        {
            var isDuplicate = await _context.Products.FindAsync(
                               Builders<Product>.Filter.Eq(p => p.Name, values.Name),
                                              null, cancellationToken);

            if (await isDuplicate.AnyAsync(cancellationToken))
            {
                return null;
            }

            var productId = ObjectId.GenerateNewId().ToString();

            var uploadResult = await _photoRepository.UploadPhoto(photoFile, cancellationToken);

            if (uploadResult is null)
            {
                return null;
            }

            values.AttachImage(uploadResult.PhotoUrl);

            var priceList = await _priceListRepository.AggregateLineItemWithProduct(
                productId,
                values.Name,
                cancellationToken);

            if (priceList is null)
            {
                return null;
            }

            values.AttachCategory(priceList.Category);

            var priceListLineItem = priceList.LineItems.SingleOrDefault(li => li.ProductId == productId);

            if (priceListLineItem is null)
            {
                return null;
            }

            values.AttachPrice(priceListLineItem.Price);

            var product = Product.Create(values, productId);

            var result = await _context.PriceLists.ReplaceOneAsync(
                pl => pl.Id == priceList.Id,
                priceList, new ReplaceOptions(), cancellationToken);

            var isSuccess = result.IsAcknowledged && result.ModifiedCount > 0;

            if (!isSuccess)
            {
                return null;
            }

            await _context.Products.InsertOneAsync(product, null, cancellationToken);

            return product;
        }

        public async Task<bool> Delete(string productId, CancellationToken cancellationToken)
        {
            var product = (await _context.Products.FindAsync(
                Builders<Product>.Filter.Eq(p => p.Id, productId),
                null, cancellationToken)).Single(cancellationToken);

            var isSuccesfullySplitedFromPriceList = await _priceListRepository.SplitLineItemFromProduct(productId, product.Category, cancellationToken);

            if (!isSuccesfullySplitedFromPriceList)
            {
                throw new ApplicationException("Cannot delete product due to it aggregation with price list");
            }

            var isSuccesfullyDeletedPhoto = await _photoRepository.DeletePhoto(product.Image.Url);

            if (!isSuccesfullyDeletedPhoto)
            {
                throw new ApplicationException("Cannot delete product due to unsuccesfull photo deletion");
            }

            var result = await _context.Products.DeleteOneAsync(
                Builders<Product>.Filter.Eq(p => p.Id, productId),
                null,
                cancellationToken);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<bool> RefreshPrice(string productId, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FindAsync(p => p.Id == productId, null, cancellationToken).Result
                .SingleOrDefaultAsync(cancellationToken);

            var priceList = await _priceListRepository.GetRetailPriceList(product.Category, cancellationToken);

            if (priceList is null)
            {
                return false;
            }

            var lineItem = priceList.LineItems.FirstOrDefault(li => li.ProductId == productId);

            if (lineItem is null)
            {
                return false;
            }

            var price = lineItem.Price;

            product.UpdatePrice(price);

            var result = await _context.Products.ReplaceOneAsync(
                Builders<Product>.Filter.Eq(p => p.Id, product.Id),
                product, new ReplaceOptions(), cancellationToken);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}