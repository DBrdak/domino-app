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

            var app = builder.Build();

            app.UseRouting();
            await app.UseOcelot();
            await app.RunAsync();
        }
    }
}