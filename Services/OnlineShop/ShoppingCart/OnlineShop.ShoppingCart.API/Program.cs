using OnlineShop.ShoppingCart.API.Extensions;
using OnlineShop.ShoppingCart.API.Middlewares;

namespace OnlineShop.ShoppingCart.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.InjectLogging();
            builder.Services.Inject(builder.Configuration);

            var app = builder.Build();

            app.MapControllers();
            app.UseCors("DefaultPolicy");
            app.UseRouting();
            app.UseMiddleware<MonitoringMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();

            await app.RunAsync();
        }
    }
}