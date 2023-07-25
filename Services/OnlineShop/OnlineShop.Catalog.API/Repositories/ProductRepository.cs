using System.Linq.Expressions;
using MongoDB.Driver;
using OnlineShop.Catalog.API.CustomTypes;
using OnlineShop.Catalog.API.Data;
using OnlineShop.Catalog.API.Entities;
using OnlineShop.Catalog.API.Extensions;

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
            (int page, string sortOrder, string sortBy, int pageSize, string category, string name,
                decimal minPrice, decimal maxPrice, bool? isAvailable, bool? isDiscounted)
        {
            var filter = ApplyFiltering(category, minPrice, maxPrice, isAvailable, isDiscounted);

            var options = ApplySorting(sortOrder, sortBy);

            var products = await (await _context.Products.FindAsync(filter, options)).ToListAsync();

            products = ApplySearch(name, products);

            return await PagedList<Product>.CreateAsync(products, page, pageSize);
        }

        private static FindOptions<Product> ApplySorting(string sortOrder, string sortBy)
        {
            var sort = sortOrder == "asc"
                ? Builders<Product>.Sort.Ascending(sortBy)
                : Builders<Product>.Sort.Descending(sortBy);
            var options = new FindOptions<Product>
            {
                Sort = sort
            };
            return options;
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

        private FilterDefinition<Product> ApplyFiltering(string category, decimal? minPrice, decimal? maxPrice, bool? isAvailable, bool? isDiscounted)
        {
            var filter = FilterDefinition<Product>.Empty;

            if (category.ToLower() == "meat" || category.ToLower() == "sausage")
            {
                filter &= Builders<Product>.Filter.Where(p => p.Category.ToLower() == category.ToLower());
            }

            if (minPrice.HasValue)
            {
                filter &= Builders<Product>.Filter.Gte(p => p.Price.Amount, minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                filter &= Builders<Product>.Filter.Lte(p => p.Price.Amount, maxPrice.Value);
            }

            if (isDiscounted.HasValue)
            {
                filter &= Builders<Product>.Filter.Eq(p => p.IsDiscounted, isDiscounted.Value);
            }

            if (isAvailable.HasValue)
            {
                filter &= Builders<Product>.Filter.Eq(p => p.IsAvailable, isAvailable.Value);
            }

            return filter;
        }
    }
}