using System.Reflection;
using MassTransit;
using OnlineShop.ShoppingCart.API.Repositories;

namespace OnlineShop.ShoppingCart.API.Extensions
{
    public static class ProgramExtensions
    {
        public static IServiceCollection Inject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = configuration["CacheSettings:ConnectionString"];
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((context, configMq) =>
                {
                    configMq.Host(configuration["EventBusSettings:HostAddress"]);
                });
            });

            return services;
        }
    }
}