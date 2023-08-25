using OnlineShop.Catalog.API.Extensions;
using OnlineShop.Catalog.API.Middlewares;

namespace OnlineShop.Catalog.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Inject(builder.Configuration);

            var app = builder.Build();

            app.UseRouting();

            app.UseMiddleware<ExceptionMiddleware>();
            app.MapControllers();
            app.UseCors("DefaultPolicy");

            await app.RunAsync();
        }
    }
}