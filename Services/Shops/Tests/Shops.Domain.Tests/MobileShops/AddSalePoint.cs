using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Date;
using Shared.Domain.Exceptions;
using Shops.Domain.MobileShops;

namespace Shops.Domain.Tests.MobileShops
{
    public class AddSalePoint
    {
        [Fact]
        public void AddSalePoint_ValidData_ShouldAdd()
        {
            // Arrange
            var shop = MobileShopTestData.CreateMobileShop();
            var location = new Location("Test Location 1", "50.2", "20.1");
            var openHours = new TimeRange("9:30", "10:45");
            var weekDay = WeekDay.Tuesday;

            // Act
            shop.AddSalePoint(location, openHours, weekDay.Value);

            // Assert
            Assert.NotEmpty(shop.SalePoints);
            Assert.True(
                shop.SalePoints[0].WeekDay == weekDay &&
                shop.SalePoints[0].IsClosed == false &&
                shop.SalePoints[0].CachedOpenHours == null &&
                shop.SalePoints[0].Location == location &&
                shop.SalePoints[0].OpenHours == openHours);
        }

        [Fact]
        public void AddSalePoint_InvalidData_ShouldThrow_DuplicateInSameDay()
        {
            // Arrange
            var shop = MobileShopTestData.CreateMobileShop();
            var location1 = new Location("Test Location 1", "50.2", "20.1");
            var openHours1 = new TimeRange("9:30", "10:45");
            var weekDay1 = WeekDay.Tuesday;
            var location2 = new Location("Test Location 1", "50.2", "20.1");
            var openHours2 = new TimeRange("11:30", "12:30");
            var weekDay2 = WeekDay.Tuesday;

            // Act
            shop.AddSalePoint(location1, openHours1, weekDay1.Value);
            var addDuplicateFunc = () => shop.AddSalePoint(location2, openHours2, weekDay2.Value);

            // Assert
            Assert.Throws<DomainException<MobileShop>>(addDuplicateFunc);
            Assert.NotEmpty(shop.SalePoints);
            Assert.Single(shop.SalePoints);
            Assert.True(
                shop.SalePoints[0].WeekDay == weekDay1 &&
                shop.SalePoints[0].IsClosed == false &&
                shop.SalePoints[0].CachedOpenHours == null &&
                shop.SalePoints[0].Location == location1 &&
                shop.SalePoints[0].OpenHours == openHours1);
            Assert.False(
                shop.SalePoints[0].WeekDay == weekDay2 &&
                shop.SalePoints[0].IsClosed == false &&
                shop.SalePoints[0].CachedOpenHours == null &&
                shop.SalePoints[0].Location == location2 &&
                shop.SalePoints[0].OpenHours == openHours2);
        }

        [Theory]
        [ClassData(typeof(AddSalePointOverlapTestData))]
        public void AddSalePoint_InvalidData_ShouldThrow_Overlaps(TimeRange openHours)
        {
            // Arrange
            var shop = MobileShopTestData.CreateMobileShop();
            var location1 = new Location("Test Location 1", "50.2", "20.1");
            var openHours1 = new TimeRange("9:30", "10:45");
            var weekDay1 = WeekDay.Tuesday;
            var location2 = new Location("Test Location 2", "51.6", "19.7");
            var openHours2 = openHours;
            var weekDay2 = WeekDay.Tuesday;

            // Act
            shop.AddSalePoint(location1, openHours1, weekDay1.Value);
            var addOverlapFunc = () => shop.AddSalePoint(location2, openHours2, weekDay2.Value);

            // Assert
            Assert.Throws<DomainException<MobileShop>>(addOverlapFunc);
            Assert.NotEmpty(shop.SalePoints);
            Assert.Single(shop.SalePoints);
            Assert.True(
                shop.SalePoints[0].WeekDay == weekDay1 &&
                shop.SalePoints[0].IsClosed == false &&
                shop.SalePoints[0].CachedOpenHours == null &&
                shop.SalePoints[0].Location == location1 &&
                shop.SalePoints[0].OpenHours == openHours1);
            Assert.False(
                shop.SalePoints[0].WeekDay == weekDay2 &&
                shop.SalePoints[0].IsClosed == false &&
                shop.SalePoints[0].CachedOpenHours == null &&
                shop.SalePoints[0].Location == location2 &&
                shop.SalePoints[0].OpenHours == openHours2);
        }
    }
}
