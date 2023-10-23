using MediatR;
using Shared.Domain.Abstractions.Entities;
using Shared.Domain.ResponseTypes;
using System.Reflection;
using Shared.Domain.Abstractions.Messaging;

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

        private static List<IDomainEvent>? RetriveDomainEvents(object obj)
        {
            var isSuccessProperty = GetProperty(obj, "IsSuccess");

            if (isSuccessProperty is null)
            {
                return null;
            }

            var isSuccess = (bool)GetValueFromProperty(obj, isSuccessProperty);

            if (!isSuccess)
            {
                return null;
            }

            var valueProperty = GetProperty(obj, "Value");

            if (valueProperty is null)
            {
                return null;
            }

            var value = GetValueFromProperty(obj, valueProperty);

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

        private static object? GetValueFromProperty(object obj, PropertyInfo property) => property.GetValue(obj);

        private static PropertyInfo? GetProperty(object obj, string propName) => obj.GetType().GetProperty(propName);
    }
}