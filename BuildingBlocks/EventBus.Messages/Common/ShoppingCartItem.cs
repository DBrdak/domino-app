namespace EventBus.Messages.Common
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
    }
}