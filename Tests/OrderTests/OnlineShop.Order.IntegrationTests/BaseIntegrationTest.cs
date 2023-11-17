using IntegrationEvents.Domain.Events.ShoppingCartCheckout;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using OnlineShop.Order.Domain.OnlineOrders;
using OnlineShop.Order.Infrastructure.Persistence;
using Renci.SshNet;
using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;
using Shared.Domain.Money;
using Shared.Domain.Quantity;
using Unit = Shared.Domain.Money.Unit;

namespace OnlineShop.Order.IntegrationTests;

public class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly OrderContext Context;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        Context = _scope.ServiceProvider.GetRequiredService<OrderContext>();

        if (!Context.Orders!.Any()
            && !Context.OrderItems.Any())
        {
            SeedDatabase();
        }
    }

    private void SeedDatabase()
    {
    }

    public class EntityFactory
    {

        //public List<OnlineOrder> CreateOrders(int count)
        //{

        //}

        private ShoppingCartCheckoutItem ProduceShoppingCartItem(int index)
        {
            var random = new Random();
            var x = random.NextInt64(3);
            var unit = x % 2 == 0 ? Unit.Kg : Unit.Pcs;
            var q = new Quantity((decimal)(random.NextDouble() * random.Next(15, 40)), unit);
            var p = new Money((decimal)(random.NextDouble() * random.Next(15, 40)), Currency.Pln, unit);

            return new ShoppingCartCheckoutItem(
                q,
                p,
                p * q,
                ObjectId.GenerateNewId().ToString(),
                $"Base Product {index}"
            );
        }

        private ShoppingCartCheckoutEvent ProduceShoppingCart(int index, int countOfItems)
        {
            var items = new List<ShoppingCartCheckoutItem>();

            for (int i = 0; i < countOfItems; i++)
            {
                items.Add(ProduceShoppingCartItem(index + i));
            }

            var shoppingCart = new ShoppingCartCheckoutEvent(
                Guid.NewGuid().ToString(),
                new Money(items.Sum(i => i.TotalValue.Amount), Currency.Pln),
                items,
                $"{index}12345678",
                $"Bob",
                $"Smith",
                new ($"Location {index}", $"20.{index}", $"52.{index}"),
                new DateTimeRange(DateTimeService.Today.AddDays(index), new TimeRange("9:00", "12:00"))
            );

            return shoppingCart;
        }
    }
}