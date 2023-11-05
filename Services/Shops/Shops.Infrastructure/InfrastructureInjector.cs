using Microsoft.Extensions.DependencyInjection;
using Shops.Domain.MobileShops;
using Shops.Domain.Shops;
using Shops.Domain.StationaryShops;
using Shops.Infrastructure.Repositories;

namespace Shops.Infrastructure
{
    public static class InfrastructureInjector
    {
        public static IServiceCollection InjectInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ShopsContext>();
            services.AddScoped<IMobileShopRepository, MobileShopRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<IStationaryShopRepository, StationaryShopRepository>();

            return services;
        }
    }
}