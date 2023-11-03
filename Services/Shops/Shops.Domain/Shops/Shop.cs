using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Shared.Domain.Abstractions.Entities;
using Shared.Domain.Exceptions;
using Shops.Domain.MobileShops;
using Shops.Domain.Shared;
using Shops.Domain.StationaryShops;

namespace Shops.Domain.Shops
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(MobileShop), typeof(StationaryShop))]
    [JsonDerivedType(typeof(MobileShop))]
    [JsonDerivedType(typeof(StationaryShop))]
    public abstract class Shop : Entity
    {
        public string ShopName { get; init; }
        public List<Seller> Sellers { get; init; }
        public List<string> OrdersId { get; init; }

        protected Shop(string shopName)
        {
            Id = ObjectId.GenerateNewId().ToString();
            ShopName = shopName;
            Sellers = new();
            OrdersId = new();
        }

        public void AddSeller(Seller seller)
        {
            if (Sellers.Any(s => s == seller))
            {
                throw new DomainException<Shop>(string.Concat(
                    $"Seller with data: ",
                    $"[First name: {seller.FirstName}, Last name:{seller.LastName}, Phone number: {seller.PhoneNumber}], ",
                    $"is already binded to shop {ShopName}"
                    ));
            }

            Sellers.Add(seller);
        }

        public void RemoveSeller(Seller seller)
        {
            if (Sellers.All(s => s != seller))
            {
                throw new DomainException<Shop>(string.Concat(
                    $"Seller with data: ",
                    $"[First name: {seller.FirstName}, Last name:{seller.LastName}, Phone number: {seller.PhoneNumber}], ",
                    $"is not binded to shop {ShopName}"
                ));
            }

            Sellers.Remove(seller);
        }

        public void AddOrder(string orderId)
        {
            if (OrdersId.Any(o => o == orderId))
            {
                throw new DomainException<Shop>(
                    $"Order with Id {orderId} already exists in order registry of shop {ShopName}");
            }

            OrdersId.Add(orderId);
        }

        public void RemoveOrder(string orderId)
        {
            OrdersId.Remove(orderId);
        }
    }
}