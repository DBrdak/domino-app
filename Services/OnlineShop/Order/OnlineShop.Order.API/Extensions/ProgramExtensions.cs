using Order.Application;
using Order.Infrastructure;

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
    }
}