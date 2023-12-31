using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OnlineShop.Order.API.Extensions;
using OnlineShop.Order.API.Middlewares;

namespace OnlineShop.Order.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.InjectLogging();
            builder.Services.Inject(builder.Configuration);

            var app = builder.Build();

            app.UseCors("DefaultPolicy");
            app.UseRouting();
            app.MapHealthChecks(
                "/health",
                new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            app.MapControllers();
            app.UseMiddleware<MonitoringMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
            await app.MigrateDatabase(app.Environment);
            await app.RunAsync();
        }
    }
}