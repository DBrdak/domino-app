using System.Reflection;
using IntegrationEvents.Domain.Results;
using MassTransit;
using OnlineShop.ShoppingCart.API.Repositories;
using FluentValidation;
using HealthChecks.ApplicationStatus.DependencyInjection;
using Serilog;

namespace OnlineShop.ShoppingCart.API.Extensions
{
    public static class ProgramExtensions
    {
        public static IServiceCollection Inject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddApplicationStatus()
                .AddRabbitMQ()
                .AddRedis(configuration["CacheSettings:ConnectionString"] ?? String.Empty);
            
            services.AddControllers();

            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = configuration["CacheSettings:ConnectionString"];
            });

            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

            services.AddMassTransit(config =>
            {
                config.AddRequestClient<CheckoutOrderResult>();
                config.UsingRabbitMq((context, configMq) =>
                {
                    configMq.Host(configuration["EventBusSettings:HostAddress"]);
                });
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

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
        
        public static WebApplicationBuilder InjectLogging(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, loggerConfig) =>
                loggerConfig.ReadFrom.Configuration(context.Configuration));

            return builder;
        }
        
    }
}