using Microsoft.Extensions.DependencyInjection;
using OnlineShop.ShoppingCart.API.Repositories;

namespace OnlineShop.ShoppingCart.Tests;

public class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    protected readonly IShoppingCartRepository Repository;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        var scope = factory.Services.CreateScope();
        Repository = scope.ServiceProvider.GetRequiredService<IShoppingCartRepository>();
    }
}