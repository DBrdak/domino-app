using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Catalog.Domain.Common
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
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Subcategory = subcategory;
            this.Category = category;
            this.ImageUrl = imageUrl;
            this.IsWeightSwitchAllowed = isWeightSwitchAllowed;
            this.SingleWeight = singleWeight;
            this.IsAvailable = isAvailable;
        }

        public void UpdatePhoto(string imageUrl)
        {
            this.ImageUrl = imageUrl;
        }
    }
}