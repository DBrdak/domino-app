using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using OnlineShop.Catalog.API;
using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.Domain.Products;
using OnlineShop.Catalog.Infrastructure;
using Testcontainers.MongoDb;

namespace OnlineShop.Catalog.IntegrationTests
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MongoDbContainer _mongoDbContainer =
            new MongoDbBuilder().WithName("mongodb").WithPortBinding(27017).Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                //.SetBasePath(Directory.)
                .AddJsonFile("D:/Programownie/Projekty/Domino Projekt/domino-app/Services/OnlineShop/Catalog/Tests/OnlineShop.Catalog.IntegrationTests/Configurations/testsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            builder.UseConfiguration(config);

            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(CatalogContext));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                var dbContext = new CatalogContext(config);

                services.AddSingleton(dbContext);
            });
        }

        public Task InitializeAsync()
            => _mongoDbContainer.StartAsync();

        public Task DisposeAsync()
            => _mongoDbContainer.DisposeAsync().AsTask();
    }
}
