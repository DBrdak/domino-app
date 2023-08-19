using Shared.Domain.Money;
using Shared.Domain.Photo;
using Shared.Domain.Quantity;

namespace EventBus.Messages.Common
{
    public class ShoppingCartItem
    {
        public Quantity Quantity { get; init; }
        public Money Price { get; init; }
        public Money TotalValue { get; init; }
        public string ProductId { get; init; }
        public string ProductName { get; init; }
        public Photo ProductImage { get; init; }

        public ShoppingCartItem(
            Quantity quantity,
            Money price,
            string productId,
            string productName,
            Photo productImage)
        {
            Quantity = quantity;
            Price = price;
            ProductId = productId;
            ProductName = productName;
            ProductImage = productImage;
        }
    }
}