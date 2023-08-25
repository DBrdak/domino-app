﻿using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Catalog.Domain;

namespace OnlineShop.Catalog.Application
{
    public static class ApplicationInjector
    {
        public static IServiceCollection InjectApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(ApplicationInjector).Assembly);
            });

            return services;
        }
    }
}