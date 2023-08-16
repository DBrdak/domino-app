using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotApiGateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            builder.Services.AddOcelot()
                .AddCacheManager(settings => settings.WithDictionaryHandle());

            builder.WebHost.ConfigureAppConfiguration((hc, cfg) =>
                cfg.AddJsonFile($"ocelot.{hc.HostingEnvironment.EnvironmentName}.json", true, true));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy",
                    builder =>
                    {
                        builder.WithOrigins(
                                "http://localhost:3000")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });

            var app = builder.Build();

            app.UseRouting();
            app.UseCors("DefaultPolicy");
            await app.UseOcelot();
            await app.RunAsync();
        }
    }
}