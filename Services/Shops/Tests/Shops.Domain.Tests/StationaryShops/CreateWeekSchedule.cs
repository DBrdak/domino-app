using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Date;
using Shared.Domain.Exceptions;
using Shops.Domain.Shared;
using Shops.Domain.StationaryShops;

namespace Shops.Domain.Tests.StationaryShops
{
    public class CreateWeekSchedule
    {
        [Fact]
        public void CreateWeekSchedule_ValidData_ShouldCreateWeekSchedule()
        {
            // Arrange
            var shop = StationaryShopTestData.CreateStationaryShop();
            var weekSchedule = new List<ShopWorkingDay>()
            {
                new (WeekDay.Monday, new (TimeOnly.Parse("8:00"), TimeOnly.Parse("20:00"))),
                new (WeekDay.Tuesday, new (TimeOnly.Parse("8:00"), TimeOnly.Parse("20:00"))),
                new (WeekDay.Wednesday, new (TimeOnly.Parse("8:00"), TimeOnly.Parse("20:00"))),
                new (WeekDay.Thursday, new (TimeOnly.Parse("8:00"), TimeOnly.Parse("20:00"))),
                new (WeekDay.Friday, new (TimeOnly.Parse("8:00"), TimeOnly.Parse("20:00"))),
                new (WeekDay.Saturday, new (TimeOnly.Parse("9:00"), TimeOnly.Parse("18:00")))
            };

            // Act
            shop.CreateWeekSchedule(weekSchedule);

            // Assert
            Assert.NotEmpty(shop.WeekSchedule);
            Assert.True(shop.WeekSchedule.Where(d => weekSchedule.Any(dd => dd.WeekDay == d.WeekDay)).SequenceEqual(weekSchedule));
        }

        [Fact]
        public void CreateWeekSchedule_InvalidData_ShouldThrow()
        {
            // Arrange
            var shop = StationaryShopTestData.CreateStationaryShop();
            var weekSchedule = new List<ShopWorkingDay>()
            {
                new (WeekDay.Monday, new (TimeOnly.Parse("8:00"), TimeOnly.Parse("20:00"))),
            };
            var duplicatedWeekSchedule = new List<ShopWorkingDay>()
            {
                new(WeekDay.Tuesday, new(TimeOnly.Parse("8:00"), TimeOnly.Parse("20:00"))),
            };

            // Act
            shop.CreateWeekSchedule(weekSchedule);
            var duplicateWeekSchduleFunc = () => shop.CreateWeekSchedule(duplicatedWeekSchedule);

            // Assert
            Assert.Throws<DomainException<StationaryShop>>(duplicateWeekSchduleFunc);
            Assert.NotEmpty(shop.WeekSchedule);
            Assert.True(shop.WeekSchedule.Where(d => weekSchedule.Any(dd => dd.WeekDay == d.WeekDay)).SequenceEqual(weekSchedule));
        }
    }
}
