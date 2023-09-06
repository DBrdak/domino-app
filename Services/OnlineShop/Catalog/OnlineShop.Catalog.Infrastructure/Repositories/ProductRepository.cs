using CloudinaryDotNet.Actions;
using MongoDB.Driver;
using OnlineShop.Catalog.Domain;
using OnlineShop.Catalog.Domain.Common;
using Shared.Domain.Money;
using Shared.Domain.ResponseTypes;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace OnlineShop.Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context)
        {
            _context = context;
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