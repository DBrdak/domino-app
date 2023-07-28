using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Order.Application.Contracts;
using OnlineShop.Order.Infrastructure.Persistence;
using OnlineShop.Order.Infrastructure.Repositories;

namespace OnlineShop.Order.Infrastructure
{
    public static class InfrastructureInjector
    {
        public static IServiceCollection InjectInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("OrderConnectionString")));

            services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));

            return services;
        }
    }
}