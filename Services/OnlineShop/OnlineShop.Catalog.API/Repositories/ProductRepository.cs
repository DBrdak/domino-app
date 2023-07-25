using MongoDB.Driver;
using OnlineShop.Catalog.API.CustomTypes;
using OnlineShop.Catalog.API.Data;
using OnlineShop.Catalog.API.Entities;

namespace OnlineShop.Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<PagedList<Product>> GetProductsAsync(int page, string sortOrder, string sortBy, int pageSize, string category)
        {
            var categoryFilter = Builders<Product>.Filter.Where(p => p.Category.ToLower() == category.ToLower());

            var sort = sortOrder == "asc" ? Builders<Product>.Sort.Ascending(sortBy) : Builders<Product>.Sort.Descending(sortBy);
            var options = new FindOptions<Product>
            {
                Sort = sort
            };

            var products = await (await _context.Products.FindAsync(categoryFilter, options)).ToListAsync();

            return await PagedList<Product>.CreateAsync(products, page, pageSize);
        }
    }
}