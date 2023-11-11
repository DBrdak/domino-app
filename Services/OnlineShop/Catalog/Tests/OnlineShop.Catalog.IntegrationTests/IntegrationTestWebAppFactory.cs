using System.Reflection;
using Castle.Components.DictionaryAdapter.Xml;
using Castle.Core.Configuration;
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

namespace OnlineShop.Catalog.IntegrationTests
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly MongoDbContainer _mongoDbContainer =
            new MongoDbBuilder().WithName("mongodb").WithPortBinding(27017).Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("D:/Programownie/Projekty/Domino Projekt/domino-app/Services/OnlineShop/Catalog/Tests/OnlineShop.Catalog.IntegrationTests/Configurations/testsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            var secretConfig = new ConfigurationBuilder()
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .AddEnvironmentVariables()
                .Build();
            builder.UseConfiguration(config);
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

                var dbContext = new CatalogContext(config);
                services.Configure<CloudinarySettings>(
                    options =>
                    {
                        secretConfig.GetSection("Cloudinary").Bind(options);
                    });

                var photoRepositoryScope = services.SingleOrDefault(s => s.ServiceType == typeof(IPhotoRepository));
                if (photoRepositoryScope is null)
                {
                    throw new TestClassException("Cannot access photo repository scope");
                }

                services.AddSingleton(dbContext);
                services.AddSingleton(photoRepositoryScope);
                services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(Program)));
            });
        }

        public Task InitializeAsync()
            => _mongoDbContainer.StartAsync();

        public Task DisposeAsync()
            => _mongoDbContainer.DisposeAsync().AsTask();
    }
}
