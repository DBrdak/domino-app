using OnlineShop.ShoppingCart.API.Models;

namespace OnlineShop.ShoppingCart.API.Entities
{
    public class ShoppingCartItem
    {
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public decimal? KgPerPcs { get; set; }
        public Money Price { get; set; }
        public decimal TotalValue => Unit.ToLower() == "kg" || !KgPerPcs.HasValue ? Price.Amount * Quantity : Price.Amount * KgPerPcs.Value * Quantity;
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }

        //public ShoppingCartItem(decimal quantity, string unit, decimal kgPerPcs, decimal price, string productId, string productName, string productImage)
        //{
        //    //TODO Użyć FluentValidation zamiast rzucać wyjątek
        //    Unit = unit == "kg" || unit == "szt" ? unit
        //        : throw new WrongInputException($"Provided unit {unit} is wrong, only \"kg\" and \"szt\" are allowed");
        //    KgPerPcs = kgPerPcs;
        //    Quantity = quantity;
        //    Price = price;
        //    ProductId = productId;
        //    ProductName = productName;
        //    ProductImage = productImage;
        //}
    }
}