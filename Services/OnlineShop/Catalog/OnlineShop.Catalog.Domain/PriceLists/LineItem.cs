using Shared.Domain.Exceptions;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.PriceLists
{
    public sealed class LineItem
    {
        public string Name { get; init; }
        public Money Price { get; private set; }
        public string? ProductId { get; private set; }

        public LineItem(string name, Money price)
        {
            if (price.Unit is null)
            {
                throw new DomainException<LineItem>("Line item of price list must have defined unit of price");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException<LineItem>("Line item of price list must have defined name");
            }
            
            Name = name;
            Price = price;
        }

        internal void UpdatePrice(Money price)
        {
            if (price.Unit is null)
            {
                throw new DomainException<PriceList>("Line item of price list must have defined unit of price");
            }
            
            Price = price;
        }

        internal void AggregateWithProduct(string productId) => ProductId = productId;

        internal void SplitFromProduct() => ProductId = null;
    }
}