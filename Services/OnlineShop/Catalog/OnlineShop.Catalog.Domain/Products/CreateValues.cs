using OnlineShop.Catalog.Domain.Shared;
using Shared.Domain.Exceptions;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.Products
{
    public sealed class CreateValues
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string? Image { get; private set; }
        public Money? Price { get; private set; }
        public Category? Category { get; private set; }
        public bool IsWeightSwitchAllowed { get; init; }
        public decimal? SingleWeight { get; init; }

        public CreateValues()
        {
        }

        public CreateValues(string name,
            string description,
            bool isWeightSwitchAllowed,
            decimal? singleWeight)
        {
            if (singleWeight is < 0)
            {
                throw new DomainException<CreateValues>("Single weight cannot be negative");
            }
            
            Name = name;
            Description = description;
            IsWeightSwitchAllowed = isWeightSwitchAllowed;
            SingleWeight = singleWeight;
        }

        public void AttachImage(string image)
        {
            Image = image;
        }

        public void AttachPrice(Money productPrice)
        {
            Price = productPrice;
        }

        public void AttachCategory(Category category)
        {
            Category = category;
        }
    }
}