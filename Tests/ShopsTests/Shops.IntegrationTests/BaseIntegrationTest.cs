using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;
using Shops.Domain.MobileShops;
using Shops.Domain.Shared;
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
            new Location("Sklep 2", "20.05", "52.24"),
            new Location("Sale Point 1","21.31", "51.73" ),
            new Location("Sale Point 2","19.97", "50.61" )
        };

        private readonly TimeRange[] _sampleTimeRanges =
        {
            new("8:00", "9:30"),
            new("8:45", "12:00"),
        };


        private readonly WeekDay[] _sampleWeekDays =
        {
            WeekDay.Friday, 
            WeekDay.Monday, 
        };

        private readonly Seller[] _sampleSellers =
        {
            new ("Joe1", "Doe1", "123456789"),
            new ("Joe2", "Doe2", null),
            new ("Joe3", "Doe3", "987654321"),
            new ("Joe4", "Doe4", null),
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
                _shop = CreateMobileShop(
                    _sampleShopNames[i + 2], 
                    _sampleVehicleNumbers[i], 
                    _sampleLocations[i + 2],
                    _sampleTimeRanges[i],
                    _sampleWeekDays[i],
                    _sampleSellers[i+2]);
                _shops.Add(_shop);
                _shop = CreateStationaryShop(_sampleShopNames[i], _sampleLocations[i], _sampleSellers[i]);
                _shops.Add(_shop);
            }
        }

        private MobileShop CreateMobileShop(
            string shopName, 
            string vehiclePlateNumber, 
            Location salePointLocation, 
            TimeRange salePointTimeRange, 
            WeekDay salePointWeekDay,
            Seller seller)
        {
            var shop = new MobileShop(shopName, vehiclePlateNumber);

            shop.AddSalePoint(salePointLocation, salePointTimeRange, salePointWeekDay.Value);

            shop.AddSeller(seller);

            return shop;
        }

        private StationaryShop CreateStationaryShop(string shopName, Location shopLocation, Seller seller)
        {
            var shop = new StationaryShop(shopName, shopLocation);

            shop.CreateWeekSchedule(new List<ShopWorkingDay>
            {
                new ShopWorkingDay(WeekDay.Monday, new("8:00", "20:00")),
                new ShopWorkingDay(WeekDay.Tuesday, new("8:00", "20:00")),
                new ShopWorkingDay(WeekDay.Wednesday, new("8:00", "20:00")),
                new ShopWorkingDay(WeekDay.Thursday, new("8:00", "20:00")),
                new ShopWorkingDay(WeekDay.Friday, new("8:00", "20:00")),
                new ShopWorkingDay(WeekDay.Saturday, new("9:00", "18:00"))
            });

            shop.AddSeller(seller);

            return shop;
        }
    }
}