﻿using Microsoft.AspNetCore.Http;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Domain.Products
{
    public interface IProductRepository
    {
        public Task<PagedList<Product>> GetProductsAsync(
            int page, string sortOrder, string sortBy, int pageSize, string category, string name,
            decimal? minPrice, decimal? maxPrice, bool? isAvailable, bool? isDiscounted, CancellationToken cancellationToken = default);

        public Task<List<Product>> GetProductsAsync(string searchPhrase, CancellationToken cancellationToken = default);

        public Task<Product?> UpdateProduct(UpdateValues newValues, CancellationToken cancellationToken = default);

        public Task<Product?> Add(CreateValues values, IFormFile productPhoto, CancellationToken cancellationToken = default);

        public Task<bool> Delete(string productId, CancellationToken cancellationToken = default);

        public Task<bool> RefreshPrice(string productId, CancellationToken cancellationToken = default);

        //TEMP
        //public Task<bool> Seed();
    }
}