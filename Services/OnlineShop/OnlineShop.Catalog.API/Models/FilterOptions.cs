namespace OnlineShop.Catalog.API.CustomTypes
{
    public class FilterOptions
    {
        public decimal MinPrice { get; private set; }
        public decimal MaxPrice { get; private set; }
        public bool IsAvailable { get; private set; }
        public bool IsDiscounted { get; private set; }
    }
}