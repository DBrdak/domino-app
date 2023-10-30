using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Domain.Abstractions.Messaging;
using Shared.Domain.ResponseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Errors;

namespace Shared.Behaviors
{
    public sealed class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TResponse : Result
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling {typeof(TRequest).Name}");

            var response = await next();

            var errorMessage = RetriveResultErrorMessage(response);

            if (errorMessage is null)
            {
                _logger.LogInformation($"Successfully handled {typeof(TRequest).Name}");
                return response;
            }

            _logger.LogWarning($"Failed to handle {typeof(TRequest).Name}: {errorMessage}");

            return response;
        }

        private static string? RetriveResultErrorMessage(object obj)
        {
            var isSuccessProperty = GetProperty(obj, "IsSuccess");

            if (isSuccessProperty is null)
            {
                return null;
            }

            var isSuccess = (bool)GetValueFromProperty(obj, isSuccessProperty);

            if (isSuccess)
            {
                return null;
            }

            var errorProperty = GetProperty(obj, "Error");

            if (errorProperty is null)
            {
                return null;
            }

            var error = GetValueFromProperty(obj, errorProperty);

            if (error is null)
            {
                return null;
            }

            return error.ToString();
        }

        private static PropertyInfo? GetProperty(object obj, string propName) => obj.GetType().GetProperty(propName);

        private static object? GetValueFromProperty(object obj, PropertyInfo property) => property.GetValue(obj);
    }
}
