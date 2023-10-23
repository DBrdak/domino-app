using Shared.Domain.Exceptions;
using System.Text.Json;

namespace OnlineShop.Catalog.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        private const string _contentType = "application/json";

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                await CatchError(e, context);
            }
        }

        private async Task CatchError<TException>(TException exception, HttpContext context)
            where TException : Exception
        {
            context.Response.ContentType = _contentType;
            int statusCode = GetStatusCodeFromException(exception);

            var json = GetResponseMessgage(exception, statusCode);

            await context.Response.WriteAsync(json);
        }

        private string GetResponseMessgage<TException>(TException exception, int statusCode)
            where TException : Exception
        {
            var response = _env.IsDevelopment()
                ? new ApiException(statusCode, exception.Message, exception.StackTrace)
                : new ApiException(statusCode, exception.Message);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);

            return json;
        }

        private static int GetStatusCodeFromException<TException>(TException exception) where TException : Exception
        {
            //TODO Exception types

            return 500;
        }
    }
}