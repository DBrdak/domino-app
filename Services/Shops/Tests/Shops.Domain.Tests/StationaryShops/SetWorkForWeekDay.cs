using Shared.Domain.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shops.Domain.Tests.StationaryShops
{
    public class SetWorkForWeekDay
    {
        [Fact]
        public void SetWorkForWeekDay_ShouldProperlySetWork()
        {
            // Arrange
            var shop = StationaryShopTestData.CreateStationaryShopWithWeekSchdule();
            var weekDayToEnable = WeekDay.Friday;

            // Act
            shop.SetWorkForWeekDay(weekDayToEnable);

            // Assert
            var workDay = shop.WeekSchedule.First(d => d.WeekDay == weekDayToEnable);
            Assert.True(!workDay.IsClosed && workDay.CachedOpenHours is null && workDay.OpenHours is not null);
        }
    }
}
