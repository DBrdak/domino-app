namespace OnlineShop.ShoppingCart.API.Middlewares
{
    using FluentValidation;
    using Microsoft.AspNetCore.Http;
    using System.Linq;
    using System.Threading.Tasks;

    public class ValidationMiddleware : IMiddleware
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationMiddleware(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var validators = _serviceProvider.GetServices<IValidator>();

            if (validators != null && validators.Any())
            {
                foreach (var validator in validators)
                {
                    var validationResult = await validator.ValidateAsync(new ValidationContext<object>(context.Request.Form.ToDictionary(x => x.Key, x => (object)x.Value.ToString())));

                    if (!validationResult.IsValid)
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";

                        var errors = validationResult.Errors.Select(error => new
                        {
                            PropertyName = error.PropertyName,
                            ErrorMessage = error.ErrorMessage
                        });

                        await context.Response.WriteAsJsonAsync(new
                        {
                            Message = "Validation errors occurred.",
                            Errors = errors
                        });

                        return;
                    }
                }
            }

            await next(context);
        }
    }
}