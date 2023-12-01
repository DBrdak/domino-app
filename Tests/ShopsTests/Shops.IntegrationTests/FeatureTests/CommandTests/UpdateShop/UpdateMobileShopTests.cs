using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shops.Application.Features.Commands.UpdateShop;
using Shops.Domain.MobileShops;
using Shops.Domain.Shared;

namespace Shops.IntegrationTests.FeatureTests.CommandTests.UpdateShop
{
    public class UpdateMobileShopTests : UpdateShopTests
    {
        private List<MobileShop> GetMobileShops => GetShops.OfType<MobileShop>().ToList();
        private readonly string _mobileShopId;

        public UpdateMobileShopTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
            _mobileShopId = GetMobileShops[0].Id;
        }

        [Fact]
        public async Task UpdateVehiclePlateNumber_ValidData_ShouldUpdate()
        {
            // Arrange
            var newVehiclePlateNumber = "WPN TT4A";
            var mobileShopUpdateValues = new MobileShopUpdateValues(
                newVehiclePlateNumber,
                null,
                null,
                null,
                null,
                null
            );
            var command = new UpdateShopCommand(
                _mobileShopId,
                null,
                null,
                mobileShopUpdateValues,
                null);

            // Act
            var result = await Sender.Send(command);
            var isPlateUpdated =
                GetMobileShops.First(s => s.Id == _mobileShopId).VehiclePlateNumber == newVehiclePlateNumber;

            // Assert
            Assert.True(isPlateUpdated);
        }

        [Fact]
        public async Task AddSalePoint_ValidData_ShouldAdd()
        {
            // Arrange
            var newSalePoint = new SalePoint(new ("Test Location", "22.22", "50.50"), new TimeRange("9:00", "10:30"), WeekDay.Thursday);
            var mobileShopUpdateValues = new MobileShopUpdateValues(
                null,
                newSalePoint,
                null,
                null,
                null,
                null
                );
            var command = new UpdateShopCommand(
                _mobileShopId,
                null,
                null,
                mobileShopUpdateValues,
                null);

            // Act
            var result = await Sender.Send(command);
            var isSalePointAdded = GetMobileShops.First(s => s.Id == _mobileShopId).SalePoints
                    .FirstOrDefault(sp => sp == newSalePoint) is not null;

            // Assert
            Assert.True(isSalePointAdded);
        }

        [Fact]
        public async Task UpdateSalePoint_ValidData_ShouldUpdate()
        {
            // Arrange
            var salePointToUpdate = GetMobileShops.First(s => s.Id == _mobileShopId).SalePoints.First();
            var updatedSalePoint = new SalePoint(
                salePointToUpdate.Location,
                WeekDay.Friday,
                salePointToUpdate.IsClosed,
                new("9:50", "11:15"),
                salePointToUpdate.CachedOpenHours,
                salePointToUpdate.Id);
            var mobileShopUpdateValues = new MobileShopUpdateValues(
                null,
                null,
                updatedSalePoint,
                null,
                null,
                null
            );
            var command = new UpdateShopCommand(
                _mobileShopId,
                null,
                null,
                mobileShopUpdateValues,
                null);

            // Act
            var result = await Sender.Send(command);
            var isSalePointUpdated = GetMobileShops.First(s => s.Id == _mobileShopId).SalePoints
                    .FirstOrDefault(sp => sp == updatedSalePoint) is not null;

            // Assert
            Assert.True(isSalePointUpdated);
        }

        [Fact]
        public async Task DeleteSalePoint_ValidData_ShouldDelete()
        {
            // Arrange
            var salePointToDelete = GetMobileShops.First(s => s.Id == _mobileShopId).SalePoints.First();
            var mobileShopUpdateValues = new MobileShopUpdateValues(
                null,
                null,
                null,
                salePointToDelete,
                null,
                null
            );
            var command = new UpdateShopCommand(
                _mobileShopId,
                null,
                null,
                mobileShopUpdateValues,
                null);

            // Act
            var result = await Sender.Send(command);
            var isSalePointDeleted = GetMobileShops.First(s => s.Id == _mobileShopId).SalePoints
                    .FirstOrDefault(sp => sp == salePointToDelete) is null;

            // Assert
            Assert.True(isSalePointDeleted);
        }
    }
}
