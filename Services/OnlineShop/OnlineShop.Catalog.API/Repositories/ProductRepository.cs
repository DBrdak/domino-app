using System.Linq.Expressions;
using OnlineShop.Catalog.API.Data;
using OnlineShop.Catalog.API.Entities;
using OnlineShop.Catalog.API.Extensions;
using OnlineShop.Catalog.API.Models;

namespace OnlineShop.Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context)
        {
            _context = context;
        }

        //TODO Rozkminić cursor pagination

        public async Task<PagedList<Product>> GetProductsAsync(int page, string sortOrder, string sortBy, int pageSize, string category, string name,
            decimal? minPrice, decimal? maxPrice, bool? isAvailable, bool? isDiscounted)
        {
            var query = _context.Set<Product>()
                .AsQueryable()
                .ApplyCategorySelect(category)
                .ApplySorting(sortBy, sortOrder)
                .ApplySearching(name)
                .ApplyFiltering(minPrice, maxPrice, isAvailable, isDiscounted);

            return await PagedList<Product>.CreateAsync(query, page, pageSize);
        }
    }

    internal static class Querying
    {
        internal static IQueryable<Product> ApplyCategorySelect(this IQueryable<Product> query, string category) =>
            query.Where(p => p.Category.ToLower() == category.ToLower());

        internal static IQueryable<Product> ApplySorting(this IQueryable<Product> query, string sortBy, string sortOrder)
        {
            if (sortBy.ToLower() == "price")
            {
                return sortOrder.ToLower() == "desc" ?
                    query.OrderByDescending(p => p.Price.Amount)
                    : query.OrderBy(p => p.Price.Amount);
            }

            return sortOrder.ToLower() == "desc" ?
                query.OrderByDescending(p => p.Name)
                : query.OrderBy(p => p.Name);
        }

        internal static IQueryable<Product> ApplyFiltering(this IQueryable<Product> query, decimal? minPrice,
            decimal? maxPrice, bool? isAvailable, bool? isDiscounted)
        {
            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price.Amount <= maxPrice);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price.Amount >= minPrice);
            }

            if (isAvailable.HasValue)
            {
                query = query.Where(p => p.IsAvailable == isAvailable);
            }

            if (isDiscounted.HasValue)
            {
                query = query.Where(p => p.IsDiscounted == isDiscounted);
            }

            return query;
        }

        internal static IQueryable<Product> ApplySearching(this IQueryable<Product> query, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return query;
            //TODO Fix ToList
            return query.ToList().Where(p =>
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
            }).AsQueryable();
        }
    }
}