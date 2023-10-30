using FluentValidation;
using MediatR;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Shared.Domain.Abstractions.Messaging;
using ValidationException = FluentValidation.ValidationException;

namespace Shared.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseCommand
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            _logger.LogInformation("Validating request {Request}", request);

            var validationFailures = await Task.WhenAll(
                _validators.Select(validator => validator.ValidateAsync(context)));

            var errors = validationFailures
                .Where(validationResult => !validationResult.IsValid)
                .SelectMany(validationResult => validationResult.Errors)
                .Select(validationFailure => new ValidationFailure(
                    validationFailure.PropertyName,
                    validationFailure.ErrorMessage))
                .ToList();

            if (errors.Any())
            {
                LogValidationFailure(request, errors);
                throw new ValidationException(errors);
            }

            _logger.LogInformation("Request {Request} validation success", request);
            var response = await next();

            return response;
        }

        private void LogValidationFailure(TRequest request, List<ValidationFailure> errors)
        {
            var errorMessages = new List<string>();

            foreach (var validationFailure in errors)
            {
                errorMessages.Add(
                    $"{validationFailure.PropertyName} attempted with value {validationFailure.AttemptedValue} failed: {validationFailure.ErrorMessage}");
            }

            _logger.LogWarning($"{request} validation failed, {string.Join('\n', errorMessages)}");
        }
    }
}