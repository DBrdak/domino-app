using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Name = name;
            Price = price;
        }

        internal void UpdatePrice(Money price) => Price = price;

        internal void AggregateWithProduct(string productId) => ProductId = productId;
    }
}