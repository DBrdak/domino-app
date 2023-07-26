using OnlineShop.ShoppingCart.API.Exceptions;

namespace OnlineShop.ShoppingCart.API.Entities
{
    public class ShoppingCartItem
    {
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public decimal KgPerPcs { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }

        public ShoppingCartItem(decimal quantity, string unit, decimal kgPerPcs, decimal price, string productId, string productName, string productImage)
        {
            Unit = unit == "kg" || unit == "szt" ? unit
                : throw new WrongInputException($"Provided unit {unit} is wrong, only \"kg\" and \"szt\" are allowed");
            KgPerPcs = kgPerPcs;
            Quantity = quantity;
            Price = price;
            ProductId = productId;
            ProductName = productName;
            ProductImage = productImage;
        }
    }
}