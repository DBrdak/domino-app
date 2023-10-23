using Newtonsoft.Json;
using Shared.Domain.Money;
using Shared.Domain.Photo;
using Shared.Domain.Quantity;

namespace OnlineShop.ShoppingCart.API.Entities
{
    public sealed class ShoppingCartItem
    {
        public Quantity Quantity { get; init; }
        public Money Price { get; init; }
        public Money TotalValue { get; init; }
        public string ProductId { get; init; }
        public string ProductName { get; init; }
        public Photo ProductImage { get; init; }
        public Quantity SingleWeight { get; init; }
        public Money? AlternativeUnitPrice { get; init; }

        [JsonConstructor]
        public ShoppingCartItem(Quantity quantity,
            Money price,
            string productId,
            string productName,
            Photo productImage,
            Quantity singleWeight,
            Money? alternativeUnitPrice)
        {
            Quantity = quantity;
            Price = price;
            TotalValue = CalculateTotalValue(quantity, price, alternativeUnitPrice);
            ProductId = productId;
            ProductName = productName;
            ProductImage = productImage;
            SingleWeight = singleWeight;
            AlternativeUnitPrice = alternativeUnitPrice;
        }

        private static Money CalculateTotalValue(Quantity quantity, Money price, Money? alternativeUnitPrice)
        {
            var isUnitAlt = quantity.Unit != price.Unit;

            if (isUnitAlt && alternativeUnitPrice != null)
            {
                return new(quantity.Value * alternativeUnitPrice.Amount, price.Currency);
            }

            return new Money(quantity.Value * price.Amount, price.Currency);
        }
    }
}