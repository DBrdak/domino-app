using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;
using Shops.Domain.Abstractions;
using Shops.Domain.Shared;

namespace Shops.Domain.MobileShops
{
    [BsonDiscriminator(nameof(MobileShop))]
    public sealed class MobileShop : Shop
    {
        public List<SalePoint> SalePoints { get; init; }
        [RegularExpression(polishVehiclePlateNumberRegex)]
        public string VehiclePlateNumber { get; init; }

        private const string polishVehiclePlateNumberRegex = "^[A-Z]{2,3}\\s[A-Z0-9]{4,5}$";

        public MobileShop(string shopName, string vehiclePlateNumber) : base(shopName)
        {
            VehiclePlateNumber = vehiclePlateNumber;
            SalePoints = new();
        }

        public void AddSalePoint(Location location, TimeRange openHours, string weekDay)
        {
            if (SalePoints.FirstOrDefault(s => s.Location == location && s.WeekDay.Value == weekDay && s.OpenHours == openHours) is {} salePointToUpdate)
            {
                UpdateSalePoint(salePointToUpdate, location, openHours,weekDay);
            }

            SalePoints.Add(new(location, openHours, WeekDay.FromValue(weekDay)));
        }

        private void UpdateSalePoint(SalePoint salePointToUpdate, Location location, TimeRange openHours, string weekDay)
        {
            SalePoints.Remove(salePointToUpdate);
            SalePoints.Add(new(location, openHours, WeekDay.FromValue(weekDay)));
        }

        public void RemoveSalePoint(SalePoint salePoint)
        {
            if (!SalePoints.Any(s => s.Location == salePoint.Location && s.WeekDay == salePoint.WeekDay && s.OpenHours == salePoint.OpenHours))
            {
                throw new ApplicationException($"Sale point doesn't exists for location: {salePoint.Location}");
            }

            SalePoints.Remove(salePoint);
        }

        public void DisableSalePoint(SalePoint salePoint)
        {
            var salePointToDisable = SalePoints.FirstOrDefault(
                                         s => s.Location == salePoint.Location &&
                                              s.WeekDay == salePoint.WeekDay &&
                                              s.OpenHours == salePoint.OpenHours) ??
                                     throw new ApplicationException(
                                         $"No sale point in location {salePoint.Location} find for shop named {ShopName}");

            salePointToDisable.Close();
        }

        public void EnableSalePoint(SalePoint salePoint)
        {
            var salePointToEnable = SalePoints.FirstOrDefault(
                                        s => s.Location == salePoint.Location &&
                                             s.WeekDay == salePoint.WeekDay &&
                                             s.OpenHours == salePoint.OpenHours) ??
                                    throw new ApplicationException(
                                        $"No sale point in location {salePoint.Location} find for shop named {ShopName}");

            salePointToEnable.Open();
        }

        public DateTimeRange? GetNextAvailabilityInSalePoint(SalePoint querySalePoint)
        {
            var salePoint = SalePoints.FirstOrDefault(sp => sp == querySalePoint) ??
                            throw new ApplicationException($"No sale point in location {querySalePoint.Location} find for shop named {ShopName}");

            if (salePoint.IsClosed)
            {
                return null;
            }

            var nextAvailableDate = DateTimeService.GetDateTimeForNextDayOfWeek(salePoint.WeekDay);

            return new(nextAvailableDate, salePoint.OpenHours);
        }
    }
}