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

            var domainEvents = RetriveDomainEvents(response) as List<IDomainEvent>;

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

        private object? RetriveDomainEvents(object obj)
        {
            var valueProperty = obj.GetType().GetProperty("Value");

            if (valueProperty is null)
            {
                return null;
            }

            var value = valueProperty.GetValue(obj);

            if (value is null)
            {
                return null;
            }

            var isEntity = value.GetType().IsSubclassOf(typeof(Entity));

            if (!isEntity)
            {
                return null;
            }

            var domainEvents = value.GetType().GetMethod("GetDomainEvents");

            if (domainEvents is null)
            {
                return null;
            }

            return domainEvents.Invoke(value, null);
        }
    }
}