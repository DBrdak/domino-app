using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Infrastructure.Persistence;

namespace Order.Infrastructure
{
    public static class InfrastructureInjector
    {
        public static IServiceCollection InjectInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("OrderConnectionString")));

            return services;
        }
    }
}