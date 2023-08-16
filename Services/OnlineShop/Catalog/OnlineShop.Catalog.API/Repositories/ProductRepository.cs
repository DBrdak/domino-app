using MongoDB.Driver;
using OnlineShop.Catalog.API.Data;
using OnlineShop.Catalog.API.Entities;
using OnlineShop.Catalog.API.Extensions;
using OnlineShop.Catalog.API.Models;

namespace OnlineShop.Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Product>> GetProductsAsync
            (int page, string sortOrder, string sortBy, int pageSize, string category, string subcategory, string name,
                decimal? minPrice, decimal? maxPrice, bool? isAvailable, bool? isDiscounted)
        {
            var filter = ApplyFiltering(category, subcategory, minPrice, maxPrice, isAvailable, isDiscounted);

            var options = ApplySorting(sortOrder, sortBy);

            var products = await (await _context.Products.FindAsync(filter, options)).ToListAsync();

            products = ApplySearch(name, products);

            return await PagedList<Product>.CreateAsync(products, page, pageSize);
        }

        private static FindOptions<Product> ApplySorting(string sortOrder, string sortBy)
        {
            sortBy = NormailzeSortPropertyName(sortBy);

            var sort = sortOrder == "asc"
                ? Builders<Product>.Sort.Ascending(sortBy)
                : Builders<Product>.Sort.Descending(sortBy);
            var options = new FindOptions<Product>
            {
                Sort = sort
            };
            return options;
        }

        private static string NormailzeSortPropertyName(string sortBy)
        {
            var normailzedSortBy = string.Empty;

            for (int i = 0; i < sortBy.Length; i++)
            {
                normailzedSortBy += i == 0 ? sortBy[i].ToString().ToUpper() : sortBy[i];
            }

            return normailzedSortBy;
        }

        private static List<Product> ApplySearch(string name, List<Product> products)
        {
            if (string.IsNullOrEmpty(name))
                return products;

            return products.Where(p =>
            {
                name = name.ToLower().RemovePolishAccents();
                var productName = p.Name.ToLower().RemovePolishAccents();

                if (productName.Contains(" "))
                {
                    var words = productName.Split(" ");
                    var result = new bool[words.Length];

                    for (int i = 0; i < words.Length; i++)
                    {
                        result[i] = words[i].StartsWith(name);
                    }

                    return result.Any(r => r == true);
                }

                return productName.StartsWith(name);
            }).ToList();
        }

        private FilterDefinition<Product> ApplyFiltering(string category, string subcategory, decimal? minPrice, decimal? maxPrice, bool? isAvailable, bool? isDiscounted)
        {
            var filter = FilterDefinition<Product>.Empty;

            if (category.ToLower() == "meat" || category.ToLower() == "sausage")
            {
                filter &= Builders<Product>.Filter.Where(p => p.Category.ToLower() == category.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(subcategory))
            {
                filter &= Builders<Product>.Filter.Where(p => p.Subcategory == subcategory);
            }

            if (minPrice is > 0)
            {
                filter &= Builders<Product>.Filter.Gte(p => p.Price.Amount, minPrice.Value);
            }

            if (maxPrice is > 0 && maxPrice > minPrice)
            {
                filter &= Builders<Product>.Filter.Lte(p => p.Price.Amount, maxPrice.Value);
            }

            if (isDiscounted == true)
            {
                filter &= Builders<Product>.Filter.Eq(p => p.IsDiscounted, true);
            }

            if (isAvailable == true)
            {
                filter &= Builders<Product>.Filter.Eq(p => p.IsAvailable, true);
            }

            return filter;
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

            var products = sausageNames.Select((name, index) => new Product
            {
                Name = name,
                Description = "Przykładowy opis wędliny",
                Category = "Sausage",
                Subcategory = subcategoriesSausage[random.Next(subcategoriesSausage.Length)],
                Image = "/assets/examples/bokmar.jpg",
                Price = new Money(Math.Round(random.Next(15, 40) + (decimal)random.NextDouble())),
                IsAvailable = random.Next(2) == 0,
                IsDiscounted = random.Next(5) == 0,
                QuantityModifier = new QuantityModifier(null)
            }).Concat(meatNames.Select((name, index) => new Product
            {
                Name = name,
                Description = "Przykładowy opis mięsa",
                Category = "Meat",
                Subcategory = "",
                Image = "/assets/examples/kark.jpg",
                Price = new Money(Math.Round(random.Next(15, 40) + (decimal)random.NextDouble())),
                IsAvailable = random.Next(2) == 0,
                IsDiscounted = random.Next(5) == 0,
                QuantityModifier = random.Next(2) == 0 ? new QuantityModifier((decimal)random.NextDouble() * (decimal)(3 - 0.7) + 0.7m) : new QuantityModifier(null)
            })).ToArray();

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