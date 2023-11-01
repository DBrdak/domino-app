using System.Reflection;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Domain.Abstractions.Entities;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.ResponseTypes;

namespace Shared.Behaviors
{
    public class DomainEventPublishBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
        where TResponse : Result
    {
        private readonly IPublisher _publisher;
        private readonly ILogger<DomainEventPublishBehavior<TRequest, TResponse>> _logger;

        public DomainEventPublishBehavior(IPublisher publisher, ILogger<DomainEventPublishBehavior<TRequest, TResponse>> logger)
        {
            _publisher = publisher;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();

            var domainEvents = RetriveDomainEvents(response);

            if (domainEvents is null)
            {
                return response;
            }

            _logger.LogInformation("The process of publishing domain events has started");

            foreach (var domainEvent in domainEvents)
            {
                _logger.LogInformation("Publishing domain event {DomainEvent}", domainEvent.GetType().Name);

                await _publisher.Publish(domainEvent, cancellationToken);

                _logger.LogInformation("Domain event {DomainEvent} published", domainEvent.GetType().Name);
            }

            _logger.LogInformation($"The process of publishing domain events is completed, {domainEvents.Count} domain events were published");

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