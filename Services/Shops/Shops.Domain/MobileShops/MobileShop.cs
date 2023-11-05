using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MongoDB.Bson.Serialization.Attributes;
using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Exceptions;
using Shared.Domain.Location;
using Shops.Domain.Shops;

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
                throw new DomainException<MobileShop>(
                    $"Sale point in location [{location.Name}] already exist for shop [{ShopName}] on {weekDay}");
            }

            if (SalePoints.Where(sp => sp.WeekDay.Value == weekDay).Any(sp => DateTimeService.IsTimeOverlap(sp.OpenHours, openHours)))
            {
                throw new DomainException<MobileShop>(
                    $"Sale point with open hours: [ {openHours} ] on {weekDay} is overlap with other sale point for shop [{ShopName}]");
            }

            SalePoints.Add(new(location, openHours, WeekDay.FromValue(weekDay)));
        }

        public void UpdateSalePoint(SalePoint updatedSalePoint)
        {
            var existingSalePoint = SalePoints.FirstOrDefault(sp => sp.Id == updatedSalePoint.Id) ??
                                    throw new DomainException<MobileShop>(
                                        $"No sale point in location {updatedSalePoint.Location} find for shop named {ShopName}");

            existingSalePoint.Update(updatedSalePoint);
        }

        public void RemoveSalePoint(SalePoint salePoint)
        {
            var salePointToRemove = SalePoints.FirstOrDefault(sp => sp.Id == salePoint.Id) ??
                                    throw new DomainException<MobileShop>($"No sale point in location {salePoint.Location} find for shop named {ShopName}");

            SalePoints.Remove(salePointToRemove);
        }

        public void DisableSalePoint(SalePoint salePoint)
        {
            var salePointToDisable = SalePoints.FirstOrDefault(s => s.Id == salePoint.Id) ??
                                     throw new DomainException<MobileShop>(
                                         $"No sale point in location {salePoint.Location} find for shop named {ShopName}");

            salePointToDisable.Close();
        }

        public void EnableSalePoint(SalePoint salePoint)
        {
            var salePointToEnable = SalePoints.FirstOrDefault(s => s.Id == salePoint.Id) ??
                                    throw new DomainException<MobileShop>(
                                        $"No sale point in location {salePoint.Location} find for shop named {ShopName}");

            salePointToEnable.Open();
        }

        public void UpdateVehicleNumberPlate(string vehicleNumberPlate)
        {
            if (!Regex.IsMatch(vehicleNumberPlate, polishVehiclePlateNumberRegex))
            {
                throw new DomainException<MobileShop>($"{vehicleNumberPlate} is invalid plate number");
            }

            VehiclePlateNumber = vehicleNumberPlate;
        }
    }
}