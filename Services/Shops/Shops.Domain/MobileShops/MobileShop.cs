using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;
using Shops.Domain.Abstractions;

namespace Shops.Domain.MobileShops
{
    public sealed class MobileShop : Shop
    {
        public List<SalePoint> SalePoints { get; init; }
        public string VehiclePlateNumber { get; init; }

        public MobileShop(string shopName, string vehiclePlateNumber) : base(shopName)
        {
            VehiclePlateNumber = vehiclePlateNumber;
            SalePoints = new();
        }

        public void AddSalePoint(Location location, TimeRange openHours, string weekDay)
        {
            SalePoints.Add(new(location, openHours, WeekDay.FromCode(weekDay)));
        }

        public void RemoveSalePoint(SalePoint salePoint)
        {
            SalePoints.Remove(salePoint);
        }

        public void DisableSalePoint(SalePoint salePoint)
        {
            var salePointToDisable = SalePoints.FirstOrDefault(sp => sp == salePoint) ??
                                     throw new ApplicationException($"No sale point [{salePoint}] find for shop named {ShopName}");

            salePointToDisable.Close();
        }

        public void EnableSalePoint(SalePoint salePoint)
        {
            var salePointToEnable = SalePoints.FirstOrDefault(sp => sp == salePoint) ??
                                     throw new ApplicationException($"No sale point [{salePoint}] find for shop named {ShopName}");

            salePointToEnable.Open();
        }

        public DateTimeRange? GetNextAvailabilityInSalePoint(SalePoint wantedSalePoint)
        {
            var salePoint = SalePoints.FirstOrDefault(sp => sp == wantedSalePoint) ??
                            throw new ApplicationException($"No sale point [{wantedSalePoint}] find for shop named {ShopName}");

            if (salePoint.IsClosed)
            {
                return null;
            }

            var nextAvailableDate = DateTimeService.GetDateTimeForNextDayOfWeek(salePoint.WeekDay);

            return new(nextAvailableDate, salePoint.OpenHours);
        }
    }
}