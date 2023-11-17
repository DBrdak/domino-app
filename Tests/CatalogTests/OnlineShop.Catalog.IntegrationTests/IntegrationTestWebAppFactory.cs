using System.Reflection;
using System.Text.Json;
using DocumentFormat.OpenXml.Wordprocessing;
using FluentValidation;
using Irony.Parsing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OnlineShop.Catalog.API;
using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.Domain.Products;
using OnlineShop.Catalog.Infrastructure;
using OnlineShop.Catalog.Infrastructure.Repositories;
using Shared.Domain.Photo;
using Testcontainers.MongoDb;
using Xunit.Sdk;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace OnlineShop.Catalog.IntegrationTests
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MongoDbContainer _mongoDbContainer =
            new MongoDbBuilder().WithName($"mongodb.{Guid.NewGuid()}").Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var secretConfig = new ConfigurationBuilder()
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();

            var configuration = BuildConfigurationForMongo();

            builder.UseConfiguration(configuration);
            builder.UseConfiguration(secretConfig);

            builder.ConfigureTestServices(services =>
            {
                var mongoDescriptor = services.SingleOrDefault(s => s.ServiceType == typeof(CatalogContext));
                var cloudinaryDescriptor = services.SingleOrDefault(
                    s => s.ServiceType.FullName ==
                         "Microsoft.Extensions.Options.IConfigureOptions`1[[Shared.Domain.Photo.CloudinarySettings, Shared.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]");

                if (mongoDescriptor is not null)
                {
                    services.Remove(mongoDescriptor);
                }

                if (cloudinaryDescriptor is not null)
                {
                    services.Remove(cloudinaryDescriptor);
                }

                var dbContext = new CatalogContext(configuration);

                services.Configure<CloudinarySettings>(
                    options =>
                    {
                        secretConfig.GetSection("Cloudinary").Bind(options);

                        if (!string.IsNullOrWhiteSpace(options.CloudName) &&
                            !string.IsNullOrWhiteSpace(options.ApiKey) &&
                            !string.IsNullOrWhiteSpace(options.ApiSecret))
                        {
                            return;
                        }

                        options.CloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUDNAME");
                        options.ApiKey = Environment.GetEnvironmentVariable("CLOUDINARY_APIKEY");
                        options.ApiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_APISECRET");
                    });

                var photoRepositoryScope = services.SingleOrDefault(s => s.ServiceType == typeof(IPhotoRepository)) ??
                                           throw new TestClassException("Cannot access photo repository scope");

                services.AddSingleton(dbContext);
                services.AddSingleton(photoRepositoryScope);
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
            var dbName = "CatalogDb";
            var collections = new
            {
                Products = "Products",
                PriceLists = "PriceLists"
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
