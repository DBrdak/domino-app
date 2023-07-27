using Microsoft.EntityFrameworkCore;
using Order.Application;
using Order.Infrastructure;
using Order.Infrastructure.Persistence;

namespace OnlineShop.Order.API.Extensions
{
    public static class ProgramExtensions
    {
        public static IServiceCollection Inject(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.InjectApplication();
            services.InjectInfrastructure(configuration);

            return services;
        }

        public static async Task<IHost> MigrateDatabase(this IHost app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<OrderContext>();
                await context.Database.MigrateAsync();
                //context.Seed();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex.Message, "Error occured during migration");
            }

            return app;
        }
    }
}