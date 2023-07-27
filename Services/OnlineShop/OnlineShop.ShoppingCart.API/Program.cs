using OnlineShop.ShoppingCart.API.Extensions;
using OnlineShop.ShoppingCart.API.Middlewares;

namespace OnlineShop.ShoppingCart.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Inject(builder.Configuration);

            var app = builder.Build();

            app.UseRouting();
            app.MapControllers();
            app.UseMiddleware<ExceptionMiddleware>();

            await app.RunAsync();
        }
    }
}