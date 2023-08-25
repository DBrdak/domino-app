using OnlineShop.Catalog.Application;
using OnlineShop.Catalog.Infrastructure;

namespace OnlineShop.Catalog.API.Extensions
{
    public static class ProgramExtensions
    {
        public static IServiceCollection Inject(this IServiceCollection services, IConfiguration configuration)
        {
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
    }
}