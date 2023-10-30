using HealthChecks.ApplicationStatus.DependencyInjection;
using IntegrationEvents.Domain.Common;
using IntegrationEvents.Domain.Events.OrderCreate;
using IntegrationEvents.Domain.Events.OrderShopQuery;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Order.API.EventBusConsumer;
using OnlineShop.Order.Application;
using OnlineShop.Order.Infrastructure;
using OnlineShop.Order.Infrastructure.Persistence;
using Serilog;

namespace OnlineShop.Order.API.Extensions
{
    public static class ProgramExtensions
    {
        public static IServiceCollection Inject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddRabbitMQ()
                .AddApplicationStatus()
                .AddNpgSql(configuration.GetConnectionString("OrderConnectionString") ?? string.Empty);
            
            services.AddControllers();
            services.InjectApplication();
            services.InjectInfrastructure(configuration);

            services.AddMassTransit(config =>
            {
                config.AddConsumer<ShoppingCartCheckoutConsumer>();
                config.AddRequestClient<OrderCreateEvent>();
                config.AddRequestClient<OrderShopQueryEvent>();
                config.UsingRabbitMq((context, configMq) =>
                {
                    configMq.Host(configuration["EventBusSettings:HostAddress"]);
                    configMq.ReceiveEndpoint(EventBusConstants.ShoppingCartCheckoutQueue,
                        configEndpoint =>
                            configEndpoint.ConfigureConsumer<ShoppingCartCheckoutConsumer>(context));
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

        public static async Task<IHost> MigrateDatabase(this IHost app, IHostEnvironment env, int? retry = 0)
        {
            var retryForAvailability = retry.Value;

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<OrderContext>();
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex.Message, "Error occured during migration");

                if (retryForAvailability < 50)
                {
                    retryForAvailability++;
                    Thread.Sleep(2000);
                    await MigrateDatabase(app, env, retryForAvailability);
                }
            }

            return app;
        }
    }
}