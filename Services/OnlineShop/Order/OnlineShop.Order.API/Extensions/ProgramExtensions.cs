using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OnlineShop.Order.API.Middlewares;
using OnlineShop.Order.Application;
using OnlineShop.Order.Infrastructure;
using OnlineShop.Order.Infrastructure.Persistence;

namespace OnlineShop.Order.API.Extensions
{
    public static class ProgramExtensions
    {
        public static IServiceCollection Inject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.InjectApplication();
            services.InjectInfrastructure(configuration);

            services.AddTransient<ExceptionHandlingMiddleware>();

            return services;
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

                if (env.IsDevelopment())
                    context.Seed();
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