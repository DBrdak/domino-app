using Microsoft.Extensions.DependencyInjection;

namespace Shops.Infrastructure
{
    public static class InfrastructureInjector
    {
        public static IServiceCollection InjectInfrastructure(this IServiceCollection services)
        {
            return services;
        }
    }
}