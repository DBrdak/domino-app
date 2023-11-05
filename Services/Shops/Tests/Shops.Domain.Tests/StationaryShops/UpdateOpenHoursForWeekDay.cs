using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;

namespace Shops.Domain.Tests.StationaryShops
{
    public class UpdateOpenHoursForWeekDay
    {
        [Theory]
        [InlineData("Poniedziałek")]
        [InlineData("Wtorek")]
        [InlineData("Środa")]
        [InlineData("Czwartek")]
        [InlineData("Piątek")]
        [InlineData("Sobota")]
        [InlineData("Niedziela")]
        public void UpdateOpenHoursForWeekDay_ValidData_ShouldUpdate(string weekDayValue)
        {
            // Arrange
            var shop = StationaryShopTestData.CreateStationaryShopWithWeekSchdule();
            var weekDay = WeekDay.FromValue(weekDayValue);
            var openHours = new TimeRange(TimeOnly.Parse("10:30"), TimeOnly.Parse("21:00"));

            // Act
            shop.UpdateOpenHoursForWeekDay(weekDay, openHours);

            // Assert
            Assert.Equal(shop.WeekSchedule.First(d => d.WeekDay == weekDay).OpenHours, openHours);
        }
    }
}
