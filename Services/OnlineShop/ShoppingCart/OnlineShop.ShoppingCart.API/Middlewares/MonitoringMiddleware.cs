using System.Diagnostics;

namespace OnlineShop.ShoppingCart.API.Middlewares
{
    public sealed class MonitoringMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MonitoringMiddleware> _logger;
        private const int longRunningRequestThreshold = 750;

        public MonitoringMiddleware(RequestDelegate next, ILogger<MonitoringMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            var stopwatch = Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();

            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            if (elapsedMilliseconds > longRunningRequestThreshold)
            {
                var message = $"Long running request: {request.Method} {request.Path} ({elapsedMilliseconds} milliseconds)";
                _logger.LogWarning(message);
            }
        }
    }
}
