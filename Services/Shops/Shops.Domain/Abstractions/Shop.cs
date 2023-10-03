using Shared.Domain.Abstractions.Entities;
using Shops.Domain.Shared;

namespace Shops.Domain.Abstractions
{
    public abstract class Shop : Entity
    {
        public string ShopName { get; init; }
        public List<Seller> Sellers { get; init; }

        protected Shop(string shopName)
        {
            ShopName = shopName;
            Sellers = new();
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public void RemoveSeller(Seller seller)
        {
            Sellers.Remove(seller);
        }
    }
}