using HealthChecks.ApplicationStatus.DependencyInjection;
using IntegrationEvents.Domain.Common;
using MassTransit;
using Serilog;
using Shops.API.EventBusConsumers;
using Shops.Application;
using Shops.Infrastructure;

namespace Shops.API.Extensions
{
    public static class ProgramExtensions
    {
        public static IServiceCollection Inject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddMongoDb(configuration.GetValue<string>("DatabaseSettings:ConnectionString") ?? string.Empty)
                .AddApplicationStatus()
                .AddRabbitMQ();
            
            services.AddControllers();

            services.InjectApplication();
            services.InjectInfrastructure();

            services.AddMassTransit(config =>
            {
                config.AddConsumer<OrderCreateConsumer>();
                config.AddConsumer<OrderDeleteConsumer>();
                config.AddConsumer<OrderShopQueryConsumer>();
                config.UsingRabbitMq((context, configMq) =>
                {
                    configMq.Host(configuration["EventBusSettings:HostAddress"]);
                    configMq.ReceiveEndpoint(
                        EventBusConstants.ShopOrderAggregationQueue,
                        configEndpoint =>
                        {
                            configEndpoint.ConfigureConsumer<OrderCreateConsumer>(context);
                            configEndpoint.ConfigureConsumer<OrderDeleteConsumer>(context);
                            configEndpoint.ConfigureConsumer<OrderShopQueryConsumer>(context);
                        });
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy",
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