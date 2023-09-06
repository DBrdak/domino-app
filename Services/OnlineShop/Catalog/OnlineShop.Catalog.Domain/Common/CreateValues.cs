using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Catalog.Domain.Common
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
            this.Name = name;
            this.Description = description;
            this.Category = category;
            this.Subcategory = subcategory;
            this.Image = image;
            this.PriceAmount = priceAmount;
            this.CurrencyCode = currencyCode;
            this.UnitCode = unitCode;
            this.IsWeightSwitchAllowed = isWeightSwitchAllowed;
            this.SingleWeight = singleWeight;
        }

        public void AttachImage(string image)
        {
            Image = image;
        }
    }
}