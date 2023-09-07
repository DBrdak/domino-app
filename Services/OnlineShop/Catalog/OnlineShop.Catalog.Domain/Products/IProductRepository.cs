using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Domain.Products
{
    public interface IProductRepository
    {
        public Task<PagedList<Product>> GetProductsAsync(
            int page, string sortOrder, string sortBy, int pageSize, string category, string subcategory, string name,
            decimal? minPrice, decimal? maxPrice, bool? isAvailable, bool? isDiscounted, CancellationToken cancellationToken = default);

        public Task<List<Product>> GetProductsAsync(string searchPhrase, CancellationToken cancellationToken = default);

        public Task<Product?> UpdateProduct(UpdateValues newValues, CancellationToken cancellationToken = default);

        public Task<Product?> Add(CreateValues values, CancellationToken cancellationToken = default);

        public Task Delete(string productId, CancellationToken cancellationToken = default);

        //TEMP
        public Task<bool> Seed();
    }
}