using CloudinaryDotNet.Actions;
using MongoDB.Driver;
using OnlineShop.Catalog.Domain;
using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.Money;
using Shared.Domain.ResponseTypes;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using OnlineShop.Catalog.Domain.PriceLists;
using Error = Shared.Domain.Errors.Error;

namespace OnlineShop.Catalog.Infrastructure.Repositories
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;
        private readonly PhotoRepository _photoRepository;
        private readonly IPriceListRepository _priceListRepository;

        public ProductRepository(CatalogContext context, PhotoRepository photoRepository, IPriceListRepository priceListRepository)
        {
            _context = context;
            _photoRepository = photoRepository;
            _priceListRepository = priceListRepository;
        }

        public async Task<PagedList<Product>> GetProductsAsync
            (int page, string sortOrder, string sortBy, int pageSize, string category, string subcategory, string name,
                decimal? minPrice, decimal? maxPrice, bool? isAvailable, bool? isDiscounted, CancellationToken cancellationToken = default)
        {
            var filter = SearchEngine.ApplyFiltering(category, subcategory, minPrice, maxPrice, isAvailable, isDiscounted);

            var options = SearchEngine.ApplySorting(sortOrder, sortBy);

            var products = await (await _context.Products.FindAsync(filter, options)).ToListAsync(cancellationToken);

            products = SearchEngine.ApplySearch(name, products);

            return await PagedList<Product>.CreateAsync(products, page, pageSize);
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
                cancellationToken)).FirstOrDefault();

            if (product is null)
            {
                return null;
            }

            if (newValues.ImageUrl != product.Image.Url)
            {
#pragma warning disable CS4014
                _photoRepository.DeletePhoto(product.Image.Url);
#pragma warning restore CS4014
            }

            product.Update(newValues);
            var result = await _context.Products.ReplaceOneAsync(
                Builders<Product>.Filter.Eq(p => p.Id, product.Id),
                product);

            return result.IsAcknowledged && result.ModifiedCount > 0 ? product : null;
        }

        public async Task<Product?> Add(CreateValues values, IFormFile photoFile, CancellationToken cancellationToken = default)
        {
            var productId = ObjectId.GenerateNewId().ToString();

            var updateResult = await _priceListRepository.AggregateLineItemWithProduct(
                productId,
                values.Name,
                cancellationToken);

            var uploadResult = await _photoRepository.UploadPhoto(photoFile);

            if (uploadResult is null ||
                updateResult is false)
            {
                return null;
            }

            var priceListLineItem = await _priceListRepository.GetLineItemForProduct(productId, cancellationToken);

            values.AttachPrice(priceListLineItem.Price);

            values.AttachImage(uploadResult.PhotoUrl);

            var product = Product.Create(values, productId);

            await _context.Products.InsertOneAsync(product);

            return product;
        }

        public async Task<bool> Delete(string productId, CancellationToken cancellationToken)
        {
            var isSuccesfullySplitedFromPriceList = await _priceListRepository.SplitLineItemFromProduct(productId, cancellationToken);

            if (!isSuccesfullySplitedFromPriceList)
            {
                throw new ApplicationException("Cannot delete product due to it aggregation with price list");
            }

            var product = (await _context.Products.FindAsync(
                Builders<Product>.Filter.Eq(p => p.Id, productId),
                null, cancellationToken)).Single();

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

        // Development Feature
        public async Task<bool> Seed()
        {
            if (await _context.Products.EstimatedDocumentCountAsync() > 0)
                return false;

            var random = new Random();

            var sausageNames = new[] { "Boczek Marysieńki", "Kiełbasa Pyszna", "Kiełbasa Krucha", "Kiełbasa Chłopska", "Salceson Królewski", "Parówki", "Frankfurterki", "Szynka Marysieńki", "Baleron Wędzony", "Kaszanka" };
            var meatNames = new[] { "Kark", "Żebro", "Schab", "Szynka Górny Zraz", "Szynka Dolny Zraz", "Polędwiczka", "Podgardle", "Pachwina", "Golonka Tylnia", "Golonka Przednia" };
            var subcategoriesSausage = new[] { "Szynka", "Kiełbasa gruba", "Kiełbasa cienka", "Podroby" };

            var products = sausageNames.Select((name, index) => Product.Create(
                name,
                "Przykładowy opis wędliny",
                "Wędlina",
                subcategoriesSausage[random.Next(subcategoriesSausage.Length)],
                "/assets/examples/bokmar.jpg",
                Math.Round(random.Next(15, 40) + (decimal)random.NextDouble()),
                "PLN",
                "kg",
                random.Next(3) == 0,
                (decimal)random.NextDouble() * (decimal)(3 - 0.7) + 0.7m))
                .Concat(meatNames.Select((name, index) => Product.Create(
                    name,
                    "Przykładowy opis mięsa",
                    "Mięso",
                    subcategoriesSausage[random.Next(subcategoriesSausage.Length)],
                    "/assets/examples/kark.jpg",
                    Math.Round(random.Next(15, 40) + (decimal)random.NextDouble()),
                    "PLN",
                    "kg",
                    random.Next(3) == 0,
                    (decimal)random.NextDouble() * (decimal)(3 - 0.7) + 0.7m))
                .ToArray()).ToList();

            var discountedProduct = products[0];
            discountedProduct.StartDiscount(7);
            products[0] = discountedProduct;

            var outOfStockProduct = products[2];
            outOfStockProduct.OutOfStock();
            products[2] = outOfStockProduct;

            var tasks = new List<Task>();

            foreach (var product in products)
            {
                tasks.Add(_context.Products.InsertOneAsync(product));
            }

            await Task.WhenAll(tasks);
            return true;
        }
    }
}