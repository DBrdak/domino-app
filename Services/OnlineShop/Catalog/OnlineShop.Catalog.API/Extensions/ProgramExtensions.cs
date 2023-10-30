using HealthChecks.ApplicationStatus.DependencyInjection;
using OnlineShop.Catalog.Application;
using OnlineShop.Catalog.Infrastructure;
using Serilog;

namespace OnlineShop.Catalog.API.Extensions
{
    public static class ProgramExtensions
    {
        public static IServiceCollection Inject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddMongoDb(configuration.GetValue<string>("DatabaseSettings:ConnectionString") ?? string.Empty)
                .AddApplicationStatus();
            
            services.AddControllers();

            services.InjectApplication();
            services.InjectInfrastructure(configuration);

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