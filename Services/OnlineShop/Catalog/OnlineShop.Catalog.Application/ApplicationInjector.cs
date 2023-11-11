using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using OnlineShop.Catalog.Application.Features.Admin.Products.Commands.DeleteProduct;
using Shared.Behaviors;

namespace OnlineShop.Catalog.Application
{
    public static class ApplicationInjector
    {
        public static IServiceCollection InjectApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(ApplicationInjector).Assembly);

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(ApplicationInjector).Assembly);

                configuration.AddOpenBehavior(typeof(DomainEventPublishBehavior<,>));
                configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
                configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            return services;
        }
    }
}