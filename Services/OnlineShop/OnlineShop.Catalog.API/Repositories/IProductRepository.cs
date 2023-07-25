using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OnlineShop.Catalog.API.CustomTypes;
using OnlineShop.Catalog.API.Entities;

namespace OnlineShop.Catalog.API.Repositories
{
    public interface IProductRepository
    {
        public Task<PagedList<Product>> GetProductsAsync(
            int page, string sortOrder, string sortBy, int pageSize, string category, string name,
            decimal minPrice, decimal maxPrice, bool? isAvailable, bool? isDiscounted);
    }
}