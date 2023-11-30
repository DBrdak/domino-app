using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Order.API;
using OnlineShop.Order.Infrastructure.Persistence;
using Shared.Domain.Photo;
using Testcontainers.PostgreSql;
using Xunit.Sdk;

namespace OnlineShop.Order.IntegrationTests
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _postgresDbContainer = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("OrderDb")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var secretConfig = new ConfigurationBuilder()
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();

            builder.UseConfiguration(secretConfig);

            builder.ConfigureTestServices(services =>
            {
                var descriptor = services
                    .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<OrderContext>));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<OrderContext>(
                    options =>
                        options
                            .UseNpgsql(_postgresDbContainer.GetConnectionString()));
            });
        }

        public async Task InitializeAsync()
            => await _postgresDbContainer.StartAsync();

        public async Task DisposeAsync()
            => await _postgresDbContainer.DisposeAsync().AsTask();
    }
}
