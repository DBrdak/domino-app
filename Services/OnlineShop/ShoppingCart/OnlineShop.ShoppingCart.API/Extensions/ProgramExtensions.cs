using MassTransit;
using OnlineShop.ShoppingCart.API.Repositories;
using System.Reflection;
using Shared.Domain.ResponseTypes;
using EventBus.Domain.Common;

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

            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

            services.AddMassTransit(config =>
            {
                config.AddRequestClient<CheckoutResult>();
                config.UsingRabbitMq((context, configMq) =>
                {
                    configMq.Host(configuration["EventBusSettings:HostAddress"]);
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "DefaultPolicy",
                    builder =>
                    {
                        builder.WithOrigins(
                                "http://localhost:3000")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });

            return services;
        }
    }
}