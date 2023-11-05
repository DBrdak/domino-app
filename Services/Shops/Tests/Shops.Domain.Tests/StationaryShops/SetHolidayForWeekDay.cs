using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Date;

namespace Shops.Domain.Tests.StationaryShops
{
    public class SetHolidayForWeekDay
    {
        [Fact]
        public void SetHolidayForWeekDay_ShouldProperlySetHoliday()
        {
            // Arrange
            var shop = StationaryShopTestData.CreateStationaryShopWithWeekSchdule();
            var weekDayToDisable = WeekDay.Friday;

            // Act
            shop.SetHolidayForWeekDay(weekDayToDisable);

            // Assert
            var holiday = shop.WeekSchedule.First(d => d.WeekDay == weekDayToDisable);
            Assert.True(holiday.IsClosed && holiday.CachedOpenHours is not null && holiday.OpenHours is null);
        }
    }
}
