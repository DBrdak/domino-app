using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.Products
{
    public sealed class CreateValues
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string Category { get; init; }
        public string Subcategory { get; init; }
        public string? Image { get; private set; }
        public Money? Price { get; private set; }
        public bool IsWeightSwitchAllowed { get; init; }
        public decimal? SingleWeight { get; init; }

        public CreateValues()
        {
        }

        public CreateValues(string name,
            string description,
            string category,
            string subcategory,
            bool isWeightSwitchAllowed,
            decimal? singleWeight)
        {
            Name = name;
            Description = description;
            Category = category;
            Subcategory = subcategory;
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
    }
}