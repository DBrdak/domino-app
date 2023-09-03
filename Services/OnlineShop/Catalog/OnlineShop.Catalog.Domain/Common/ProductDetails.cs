using Shared.Domain.Money;
using Shared.Domain.Quantity;

namespace OnlineShop.Catalog.Domain.Common
{
    public sealed class ProductDetails
    {
        public bool IsAvailable { get; private set; }
        public bool IsDiscounted { get; private set; }
        public bool IsWeightSwitchAllowed { get; init; }
        public Quantity? SingleWeight { get; init; }

        public ProductDetails(bool isAvailable, bool isDiscounted, bool isWeightSwitchAllowed, decimal? singleWeight)
        {
            IsAvailable = isAvailable;
            IsDiscounted = isDiscounted;
            IsWeightSwitchAllowed = isWeightSwitchAllowed;

            if (isWeightSwitchAllowed && !singleWeight.HasValue)
            {
                throw new ApplicationException("Single weight is not provided");
            }

            SingleWeight = isWeightSwitchAllowed ? new(singleWeight!.Value, Unit.Kg) : null;
        }

        public void StartDiscount() => IsDiscounted = true;

        public void EndDiscount() => IsDiscounted = false;

        public void Available() => IsAvailable = true;

        public void Unavailable() => IsDiscounted = false;
    }
}