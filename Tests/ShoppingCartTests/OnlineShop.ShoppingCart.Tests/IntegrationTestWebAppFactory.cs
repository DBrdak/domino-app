using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using OnlineShop.ShoppingCart.API.Repositories;
using Testcontainers.Redis;
using Program = OnlineShop.ShoppingCart.API.Program;

namespace OnlineShop.ShoppingCart.Tests
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly RedisContainer _redisContainer =
            new RedisBuilder().WithName($"redis.{Guid.NewGuid()}").Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var configuration = BuildConfigurationForRedis();

            builder.UseConfiguration(configuration);

            builder.ConfigureTestServices(services =>
            {
                var cacheDescriptor = services.SingleOrDefault(s => s.ServiceType == typeof(IDistributedCache));

                if (cacheDescriptor is not null)
                {
                    services.Remove(cacheDescriptor);
                }

                services.AddStackExchangeRedisCache(opt =>
                {
                    opt.Configuration = configuration["cacheSettings:ConnectionString"];
                });
            });
        }

        public Task InitializeAsync()
            => _redisContainer.StartAsync();

        public Task DisposeAsync()
            => _redisContainer.DisposeAsync().AsTask();

        private string CreateJsonConfiguration(object cacheSettings)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(new { cacheSettings });

            return json;
        }

        private IConfiguration BuildConfigurationForRedis()
        {
            var connectionString = _redisContainer.GetConnectionString();

            var cacheSettings = new
            {
                ConnectionString = connectionString
            };

            string json = CreateJsonConfiguration(cacheSettings);

            var builder = new ConfigurationBuilder();
            builder.AddJsonStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)));
            return builder.Build();
        }
    }
}
