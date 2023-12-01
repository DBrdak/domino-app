using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shops.Application.Features.Commands.UpdateShop;
using Shops.Domain.StationaryShops;

namespace Shops.IntegrationTests.FeatureTests.CommandTests.UpdateShop
{
    public class UpdateStationaryShopTests : UpdateShopTests
    {
        private readonly string _stationaryShopId;
        private List<StationaryShop> GetStationaryShops => GetShops.OfType<StationaryShop>().ToList();

        public UpdateStationaryShopTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
            _stationaryShopId = GetStationaryShops[0].Id;
        }

        [Fact]
        public async Task UpdateWeekDay_ValidData_ShouldUpdate()
        {
            // Arrange
            var weekDayToUpdate = WeekDay.Monday;
            var newWorkingHours = new TimeRange("8:00", "18:00");
            var stationaryShopUpdateValues = new StationaryShopUpdateValues(
                null,
                weekDayToUpdate, 
                newWorkingHours,
                null,
                null
            );
            var command = new UpdateShopCommand(
                _stationaryShopId,
                null,
                null,
                null,
                stationaryShopUpdateValues);

            // Act
            var result = await Sender.Send(command);
            var isWeekDayUpdated = GetStationaryShops.First(s => s.Id == _stationaryShopId).WeekSchedule
                .First(d => d.WeekDay == weekDayToUpdate).OpenHours == newWorkingHours;

            // Assert
            Assert.True(isWeekDayUpdated);
        }
    }
}
