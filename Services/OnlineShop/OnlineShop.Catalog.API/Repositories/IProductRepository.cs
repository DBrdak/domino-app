using OnlineShop.Catalog.API.Entities;
using OnlineShop.Catalog.API.Models;

namespace OnlineShop.Catalog.API.Repositories
{
    public interface IProductRepository
    {
        public Task<PagedList<Product>> GetProductsAsync(
            int page, string sortOrder, string sortBy, int pageSize, string category, string subcategory, string name,
            decimal? minPrice, decimal? maxPrice, bool? isAvailable, bool? isDiscounted);
    }
}