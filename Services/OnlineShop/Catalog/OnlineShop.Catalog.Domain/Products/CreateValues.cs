using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Catalog.Domain.Products
{
    public sealed class CreateValues
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string Category { get; init; }
        public string Subcategory { get; init; }
        public string Image { get; private set; }
        public decimal PriceAmount { get; init; }
        public string CurrencyCode { get; init; }
        public string UnitCode { get; init; }
        public bool IsWeightSwitchAllowed { get; init; }
        public decimal? SingleWeight { get; init; }

        public CreateValues(string name,
            string description,
            string category,
            string subcategory,
            string image,
            decimal priceAmount,
            string currencyCode,
            string unitCode,
            bool isWeightSwitchAllowed,
            decimal? singleWeight)
        {
            Name = name;
            Description = description;
            Category = category;
            Subcategory = subcategory;
            Image = image;
            PriceAmount = priceAmount;
            CurrencyCode = currencyCode;
            UnitCode = unitCode;
            IsWeightSwitchAllowed = isWeightSwitchAllowed;
            SingleWeight = singleWeight;
        }

        public void AttachImage(string image)
        {
            Image = image;
        }
    }
}