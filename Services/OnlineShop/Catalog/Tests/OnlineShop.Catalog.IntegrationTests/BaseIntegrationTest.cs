using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Catalog.Infrastructure;

namespace OnlineShop.Catalog.IntegrationTests;

public class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly CatalogContext Context;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        Context = _scope.ServiceProvider.GetRequiredService<CatalogContext>();
    }
}