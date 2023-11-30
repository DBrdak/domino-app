using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;
using Shops.Domain.MobileShops;
using Shops.Domain.Shops;
using Shops.Domain.StationaryShops;
using Shops.Infrastructure;

namespace Shops.IntegrationTests;

public class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly ShopsContext Context;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        Context = _scope.ServiceProvider.GetRequiredService<ShopsContext>();

        if (Context.Shops.EstimatedDocumentCount() < 1)
        {
            SeedDatabase();
        }
    }

    private void SeedDatabase()
    {
        var factory = new EntityFactory();
        var shops = factory.GetShops();

        Context.Shops.InsertMany(shops);
    }

    public class EntityFactory
    {
        private readonly List<Shop> _shops;
        private Shop? _shop;
        private readonly string[] _sampleShopNames = 
        {
            "Sklep 1",
            "Sklep 2",
            "Sklep 3",
            "Sklep 4",
        };

        private readonly string[] _sampleVehicleNumbers = 
        {
            "WPN 21LE",
            "WPN 33EA",
        };

        private readonly Location[] _sampleLocations = 
        {
            new Location("Sklep 1", "20.65", "52.64"),
            new Location("Sklep 2", "20.05", "52.24")
        };

        public EntityFactory()
        {
            _shops = new List<Shop>();
            _shop = null;

            CreateShops();
        }

        public List<Shop> GetShops() => _shops;

        private void CreateShops()
        {
            for (int i = 0; i < _sampleShopNames.Length / 2; i++)
            {
                _shop = CreateMobileShop(_sampleShopNames[i + 2], _sampleVehicleNumbers[i]);
                _shops.Add(_shop);
                _shop = CreateStationaryShop(_sampleShopNames[i], _sampleLocations[i]);
                _shops.Add(_shop);
            }
        }

        private MobileShop CreateMobileShop(string shopName, string vehiclePlateNumber)
        {
            var shop = new MobileShop(shopName, vehiclePlateNumber);

            shop.AddSalePoint(new Location("Location 1", "23.12", "51.98"), new TimeRange("8:30", "11:45"), WeekDay.Saturday.Value);
        }

        private StationaryShop CreateStationaryShop(string shopName, Location shopLocation) => new (shopName, shopLocation);
    }
}