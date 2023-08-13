using System.ComponentModel.Design;
using System.Reflection;
using EventBus.Messages.Common;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Net.Http.Headers;
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
                config.AddRequestClient<CheckoutResultResponse>();
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