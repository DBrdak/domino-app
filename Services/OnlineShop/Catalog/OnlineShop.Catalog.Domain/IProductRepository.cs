using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Domain
{
    public interface IProductRepository
    {
        public Task<PagedList<Product>> GetProductsAsync(
            int page,
            string sortOrder,
            string sortBy,
            int pageSize,
            string category,
            string subcategory,
            string name,
            decimal? minPrice,
            decimal? maxPrice,
            bool? isAvailable,
            bool? isDiscounted);
    }
}