namespace OnlineShop.Catalog.Domain.Products
{
    public sealed class UpdateValues
    {
        public string Id { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; set; }
        public bool IsWeightSwitchAllowed { get; init; }
        public decimal? SingleWeight { get; init; }
        public bool IsAvailable { get; init; }

        public UpdateValues()
        {
            
        }

        public UpdateValues(string id,
            string description,
            string imageUrl,
            bool isWeightSwitchAllowed,
            decimal? singleWeight,
            bool isAvailable)
        {
            Id = id;
            Description = description;
            ImageUrl = imageUrl;
            IsWeightSwitchAllowed = isWeightSwitchAllowed;
            SingleWeight = singleWeight;
            IsAvailable = isAvailable;
        }

        public void UpdatePhoto(string imageUrl)
        {
            ImageUrl = imageUrl;
        }
    }
}