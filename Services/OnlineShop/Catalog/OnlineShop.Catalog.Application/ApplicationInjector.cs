using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Catalog.Application.Behaviors;
using System.Reflection;

namespace OnlineShop.Catalog.Application
{
    public static class ApplicationInjector
    {
        public static IServiceCollection InjectApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(ApplicationInjector).Assembly);

                configuration.AddOpenBehavior(typeof(DomainEventPublishBehavior<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(ApplicationInjector)));

            return services;
        }
    }
}