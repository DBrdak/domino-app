using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Catalog.Domain.Products
{
    public sealed class UpdateValues
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Subcategory { get; init; }
        public string Category { get; init; }
        public string ImageUrl { get; private set; }
        public bool IsWeightSwitchAllowed { get; init; }
        public decimal? SingleWeight { get; init; }
        public bool IsAvailable { get; init; }

        public UpdateValues(string id,
            string name,
            string description,
            string subcategory,
            string category,
            string imageUrl,
            bool isWeightSwitchAllowed,
            decimal? singleWeight,
            bool isAvailable)
        {
            Id = id;
            Name = name;
            Description = description;
            Subcategory = subcategory;
            Category = category;
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