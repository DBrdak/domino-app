using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Order.Application
{
    public static class ApplicationInjector
    {
        public static IServiceCollection InjectApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}