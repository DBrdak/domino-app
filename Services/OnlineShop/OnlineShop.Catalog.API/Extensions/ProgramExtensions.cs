using System.Globalization;
using Microsoft.AspNetCore.Localization;
using OnlineShop.Catalog.API.Data;
using OnlineShop.Catalog.API.Repositories;

namespace OnlineShop.Catalog.API.Extensions
{
    public static class ProgramExtensions
    {
        public static IServiceCollection Inject(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<ICatalogContext, CatalogContext>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}