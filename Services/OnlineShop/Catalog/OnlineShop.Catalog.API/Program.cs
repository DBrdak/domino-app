using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OnlineShop.Catalog.API.Extensions;
using OnlineShop.Catalog.API.Middlewares;
using Serilog;

namespace OnlineShop.Catalog.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.InjectLogging();
            builder.Services.Inject(builder.Configuration);

            var app = builder.Build();

            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.MapHealthChecks(
                "/health",
                new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            app.MapControllers();
            app.UseCors("DefaultPolicy");
            app.UseMiddleware<MonitoringMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();

            await app.RunAsync();
        }
    }
}