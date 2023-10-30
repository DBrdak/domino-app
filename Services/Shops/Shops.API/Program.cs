using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Shops.API.Extensions;
using Shops.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.InjectLogging();
builder.Services.Inject(builder.Configuration);

var app = builder.Build();

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