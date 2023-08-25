using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Catalog.Domain;
using OnlineShop.Catalog.Infrastructure.Repositories;

namespace OnlineShop.Catalog.Infrastructure
{
    public static class InfrastructureInjector
    {
        public static IServiceCollection InjectInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<CatalogContext>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}