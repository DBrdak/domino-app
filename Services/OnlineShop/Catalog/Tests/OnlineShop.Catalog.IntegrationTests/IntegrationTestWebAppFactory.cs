using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using OnlineShop.Catalog.API;
using OnlineShop.Catalog.Infrastructure;
using Testcontainers.MongoDb;

namespace OnlineShop.Catalog.IntegrationTests
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MongoDbContainer _mongoDbContainer =
            new MongoDbBuilder().WithName("mongodb").Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(MongoClient));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                var client = new MongoClient(_mongoDbContainer.GetConnectionString());

                services.AddSingleton(client);
            });
        }

        public Task InitializeAsync()
            => _mongoDbContainer.StartAsync();

        public Task DisposeAsync()
            => _mongoDbContainer.DisposeAsync().AsTask();
    }
}
