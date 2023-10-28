using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
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
        public string VehiclePlateNumber { get; private set; }

        private const string polishVehiclePlateNumberRegex = "^[A-Z]{2,3}\\s[A-Z0-9]{4,5}$";

        public MobileShop(string shopName, string vehiclePlateNumber) : base(shopName)
        {
            VehiclePlateNumber = vehiclePlateNumber;
            SalePoints = new();
        }

        public void AddSalePoint(Location location, TimeRange openHours, string weekDay)
        {
            if (SalePoints.Any(sp => sp.Location.Name == location.Name && sp.WeekDay.Value == weekDay))
            {
                throw new ApplicationException(
                    $"Sale point in location [{location.Name}] already exist for shop [{ShopName}] on {weekDay}");
            }

            SalePoints.Add(new(location, openHours, WeekDay.FromValue(weekDay)));
        }

        public void UpdateSalePoint(SalePoint updatedSalePoint)
        {
            var existingSalePoint = SalePoints.FirstOrDefault(sp => sp.Id == updatedSalePoint.Id) ??
                                    throw new ApplicationException(
                                        $"No sale point in location {updatedSalePoint.Location} find for shop named {ShopName}");

            existingSalePoint.Update(updatedSalePoint);
        }

        public void RemoveSalePoint(SalePoint salePoint)
        {
            var salePointToRemove = SalePoints.FirstOrDefault(sp => sp.Id == salePoint.Id) ??
                                    throw new ApplicationException($"No sale point in location {salePoint.Location} find for shop named {ShopName}");

            SalePoints.Remove(salePointToRemove);
        }

        public void DisableSalePoint(SalePoint salePoint)
        {
            var salePointToDisable = SalePoints.FirstOrDefault(s => s.Id == salePoint.Id) ??
                                     throw new ApplicationException(
                                         $"No sale point in location {salePoint.Location} find for shop named {ShopName}");

            salePointToDisable.Close();
        }

        public void EnableSalePoint(SalePoint salePoint)
        {
            var salePointToEnable = SalePoints.FirstOrDefault(s => s.Id == salePoint.Id) ??
                                    throw new ApplicationException(
                                        $"No sale point in location {salePoint.Location} find for shop named {ShopName}");

            salePointToEnable.Open();
        }

        public void UpdateVehicleNumberPlate(string vehicleNumberPlate)
        {
            if (!Regex.IsMatch(vehicleNumberPlate, polishVehiclePlateNumberRegex))
            {
                throw new ApplicationException($"{vehicleNumberPlate} is invalid plate number");
            }

            VehiclePlateNumber = vehicleNumberPlate;
        }
    }
}