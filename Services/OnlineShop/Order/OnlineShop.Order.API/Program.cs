using OnlineShop.Order.API.Extensions;
using OnlineShop.Order.API.Middlewares;

namespace OnlineShop.Order.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Inject(builder.Configuration);

            var app = builder.Build();

            app.UseCors("DefaultPolicy");
            app.UseRouting();
            app.MapControllers();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            await app.MigrateDatabase(app.Environment);
            await app.RunAsync();
        }
    }
}