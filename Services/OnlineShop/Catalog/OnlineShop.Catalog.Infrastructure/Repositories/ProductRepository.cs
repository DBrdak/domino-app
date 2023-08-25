using MongoDB.Driver;
using OnlineShop.Catalog.Domain;
using OnlineShop.Catalog.Domain.Common;
using Shared.Domain.Money;
using Shared.Domain.ResponseTypes;
using System.Globalization;
using System.Text;

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
            var filter = ApplyFiltering(category, subcategory, minPrice, maxPrice, isAvailable, isDiscounted);

            var options = ApplySorting(sortOrder, sortBy);

            var products = await (await _context.Products.FindAsync(filter, options, cancellationToken)).ToListAsync();

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
            if (string.IsNullOrWhiteSpace(name))
                return products;

            return products.Where(p =>
            {
                name = RemovePolishAccents(name.ToLower());
                var productName = RemovePolishAccents(p.Name.ToLower());

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
                filter &= Builders<Product>.Filter.Eq(p => p.Category, Category.FromValue(category));
            }
            else
            {
                throw new ApplicationException("Category is required");
            }

            if (!string.IsNullOrWhiteSpace(subcategory))
            {
                filter &= Builders<Product>.Filter.Where(p => p.Subcategory == subcategory);
            }

            if (minPrice is > 0)
            {
                filter &= Builders<Product>.Filter.Where(
                    p => p.DiscountedPrice == null ? p.Price.Amount >= minPrice : p.DiscountedPrice.Amount >= minPrice);
            }

            if (maxPrice is > 0 && maxPrice > minPrice)
            {
                filter &= Builders<Product>.Filter.Where(
                    p => p.DiscountedPrice == null ? p.Price.Amount <= maxPrice : p.DiscountedPrice.Amount <= maxPrice);
            }

            if (isDiscounted == true)
            {
                filter &= Builders<Product>.Filter.Eq(p => p.Details.IsDiscounted, true);
            }

            if (isAvailable == true)
            {
                filter &= Builders<Product>.Filter.Eq(p => p.Details.IsAvailable, true);
            }

            return filter;
        }

        private static string RemovePolishAccents(string input)
        {
            string normalized = input.Normalize(NormalizationForm.FormD);
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < normalized.Length; i++)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(normalized[i]) != UnicodeCategory.NonSpacingMark)
                {
                    if (normalized[i] != 322)
                        result.Append(normalized[i]);
                    else
                        result.Append('l');
                }
            }

            return result.ToString();
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
                    "/assets/examples/kark.jpeg",
                    Math.Round(random.Next(15, 40) + (decimal)random.NextDouble()),
                    "PLN",
                    "kg",
                    random.Next(3) == 0,
                    (decimal)random.NextDouble() * (decimal)(3 - 0.7) + 0.7m))
                .ToArray());

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