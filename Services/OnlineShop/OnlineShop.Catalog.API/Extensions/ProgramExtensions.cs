using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineShop.Catalog.API.Data;
using OnlineShop.Catalog.API.Repositories;

namespace OnlineShop.Catalog.API.Extensions
{
    public static class ProgramExtensions
    {
        public static IServiceCollection Inject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddDbContext<CatalogContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("CatalogConnectionString")));

            return services;
        }

        public static async Task<IHost> MigrateDatabase(this IHost app, IHostEnvironment env, int? retry = 0)
        {
            var retryForAvailability = retry.Value;

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<CatalogContext>();
                await context.Database.MigrateAsync();

                if (env.IsDevelopment())
                    context.SeedData();
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