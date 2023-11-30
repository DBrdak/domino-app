using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Shared.Domain.Photo;
using Testcontainers.MongoDb;
using Xunit.Sdk;
using Shops.API;
using Shops.Infrastructure;
using Program = Shops.API.Program;

namespace Shops.IntegrationTests
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MongoDbContainer _mongoDbContainer =
            new MongoDbBuilder().WithName($"mongodb.{Guid.NewGuid()}").Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var configuration = BuildConfigurationForMongo();

            builder.UseConfiguration(configuration);

            builder.ConfigureTestServices(services =>
            {
                var mongoDescriptor = services.SingleOrDefault(s => s.ServiceType == typeof(ShopsContext));

                if (mongoDescriptor is not null)
                {
                    services.Remove(mongoDescriptor);
                }

                var dbContext = new ShopsContext(configuration);

                services.AddSingleton(dbContext);
            });
        }

        public Task InitializeAsync()
            => _mongoDbContainer.StartAsync();

        public Task DisposeAsync()
            => _mongoDbContainer.DisposeAsync().AsTask();

        private string CreateJsonConfiguration(object dataBaseSettings)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(new { dataBaseSettings });

            return json;
        }

        private IConfiguration BuildConfigurationForMongo()
        {
            var connectionString = _mongoDbContainer.GetConnectionString();
            var dbName = "ShopsDb";
            var collections = new
            {
                Shops = "Shops"
            };

            var databaseSettings = new
            {
                ConnectionString = connectionString,
                DatabaseName = dbName,
                Collections = collections
            };

            string json = CreateJsonConfiguration(databaseSettings);

            var builder = new ConfigurationBuilder();
            builder.AddJsonStream(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)));
            return builder.Build();
        }
    }
}
