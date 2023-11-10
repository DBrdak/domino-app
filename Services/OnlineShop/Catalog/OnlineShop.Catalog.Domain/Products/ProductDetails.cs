using System.Text.Json.Serialization;
using Shared.Domain.Exceptions;
using Shared.Domain.Money;
using Shared.Domain.Quantity;

namespace OnlineShop.Catalog.Domain.Products
{
    public sealed class ProductDetails
    {
        public bool IsAvailable { get; private set; }
        public bool IsDiscounted { get; private set; }
        public bool IsWeightSwitchAllowed { get; private set; }
        public Quantity? SingleWeight { get; private set; }

        public ProductDetails(bool isAvailable, bool isDiscounted, bool isWeightSwitchAllowed, decimal? singleWeight)
        {
            IsAvailable = isAvailable;
            IsDiscounted = isDiscounted;
            IsWeightSwitchAllowed = isWeightSwitchAllowed;

            if (isWeightSwitchAllowed && !singleWeight.HasValue)
            {
                throw new DomainException<ProductDetails>("Single weight is not provided");
            }

            SingleWeight = isWeightSwitchAllowed ? new(singleWeight!.Value, Unit.Kg) : null;
        }

        [JsonConstructor]
        public ProductDetails()
        {}

        public void StartDiscount() => IsDiscounted = true;

        public void EndDiscount() => IsDiscounted = false;

        public void Available() => IsAvailable = true;

        public void Unavailable() => IsAvailable = false;

        public void AllowWeightSwitch(decimal weight, Unit unit)
        {
            IsWeightSwitchAllowed = true;
            SingleWeight = new(weight, unit);
        }

        public void ForbidWeightSwitch()
        {
            IsWeightSwitchAllowed = false;
            SingleWeight = null;
        }
    }
}