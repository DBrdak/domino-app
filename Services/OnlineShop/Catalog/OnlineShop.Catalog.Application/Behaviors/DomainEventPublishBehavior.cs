using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OnlineShop.Catalog.Application.Abstractions.Messaging;
using Shared.Domain.Abstractions;
using Shared.Domain.ResponseTypes;

namespace OnlineShop.Catalog.Application.Behaviors
{
    internal class DomainEventPublishBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
        where TResponse : Result
    {
        private readonly IPublisher _publisher;

        public DomainEventPublishBehavior(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();

            var domainEvents = RetriveDomainEvents(response);

            if (domainEvents is null)
            {
                return response;
            }

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            return response;
        }

        private List<IDomainEvent>? RetriveDomainEvents(object obj)
        {
            var valueProperty = GetValueProperty(obj);

            if (valueProperty is null)
            {
                return null;
            }

            var value = GetValueFromValueProperty(obj, valueProperty);

            if (value is null)
            {
                return null;
            }

            var isEntity = IsEntityType(value);

            if (!isEntity)
            {
                return null;
            }

            var domainEventsMethod = GetDomainEventsMethod(value);

            if (domainEventsMethod is null)
            {
                return null;
            }

            var domainEvents = GetDomainEvents(domainEventsMethod, value);

            return domainEvents;
        }

        private static List<IDomainEvent>? GetDomainEvents(MethodInfo domainEventsMethod, object value) =>
            domainEventsMethod.Invoke(value, null) as List<IDomainEvent>;

        private static MethodInfo? GetDomainEventsMethod(object value) => value.GetType().GetMethod("GetDomainEvents");

        private static bool IsEntityType(object value) => value.GetType().IsSubclassOf(typeof(Entity));

        private static object? GetValueFromValueProperty(object obj, PropertyInfo valueProperty) => valueProperty.GetValue(obj);

        private static PropertyInfo? GetValueProperty(object obj) => obj.GetType().GetProperty("Value");
    }
}