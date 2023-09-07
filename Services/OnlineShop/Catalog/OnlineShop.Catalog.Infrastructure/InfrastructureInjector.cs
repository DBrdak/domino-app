﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Catalog.Domain;
using OnlineShop.Catalog.Infrastructure.Repositories;
using Shared.Domain.Photo;

using Microsoft.Extensions.DependencyInjection;

using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.Domain.Products;

namespace OnlineShop.Catalog.Infrastructure
{
    public static class InfrastructureInjector
    {
        public static IServiceCollection InjectInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CloudinarySettings>(options =>
            {
                configuration.GetSection("Cloudinary").Bind(options);
            });

            services.AddScoped<CatalogContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPriceListRepository, PriceListRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();

            return services;
        }
    }
}