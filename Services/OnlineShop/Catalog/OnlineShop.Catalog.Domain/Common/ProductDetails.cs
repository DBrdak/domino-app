namespace OnlineShop.Catalog.Domain.Common
{
    public sealed class ProductDetails
    {
        public bool IsAvailable { get; private set; }
        public bool IsDiscounted { get; private set; }
        public bool IsWeightSwitchAllowed { get; init; }
        public decimal? SingleWeight { get; init; }

        public ProductDetails(bool isAvailable, bool isDiscounted, bool isWeightSwitchAllowed, decimal? singleWeight)
        {
            IsAvailable = isAvailable;
            IsDiscounted = isDiscounted;
            IsWeightSwitchAllowed = isWeightSwitchAllowed;
            SingleWeight = isWeightSwitchAllowed ? singleWeight : null;
        }

        public void StartDiscount() => IsDiscounted = true;

        public void EndDiscount() => IsDiscounted = false;

        public void Available() => IsAvailable = true;

        public void Unavailable() => IsDiscounted = false;
    }
}