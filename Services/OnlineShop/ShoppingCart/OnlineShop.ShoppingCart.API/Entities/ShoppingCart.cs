using Shared.Domain.Money;

namespace OnlineShop.ShoppingCart.API.Entities
{
    public class ShoppingCart
    {
        public string ShoppingCartId { get; init; }
        public List<ShoppingCartItem> Items { get; init; }

        public Money TotalPrice
        {
            get
            {
                if (Items is null || !Items.Any())
                    return new(0, Currency.Pln);

                var currency = Items.FirstOrDefault()?.Price.Currency ??
                               throw new ArgumentException("Items currency not provided");

                if (Items.Any(i => i.Price.Currency != currency))
                    throw new ArgumentException("Items currency inconsistency");

                var totalAmount = Items.Sum(i => i.TotalValue.Amount);

                return new Money(totalAmount, currency);
            }
        }

        public ShoppingCart(string shoppingCartId)
        {
            ShoppingCartId = shoppingCartId;
            Items = new List<ShoppingCartItem>();
        }

        public ShoppingCart(string shoppingCartId, List<ShoppingCartItem> items)
        {
            ShoppingCartId = shoppingCartId;
            Items = items;
        }

        public ShoppingCart()
        { }
    }
}