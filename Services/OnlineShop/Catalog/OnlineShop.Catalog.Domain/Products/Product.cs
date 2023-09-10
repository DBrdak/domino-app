using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.Domain.Products.Events;
using Shared.Domain.Abstractions;
using Shared.Domain.Money;
using Shared.Domain.Photo;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineShop.Catalog.Domain.Products
{
    public sealed class Product : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Category Category { get; private set; }
        public string Subcategory { get; private set; }
        public Photo Image { get; private set; }
        public Money Price { get; init; }
        public ProductDetails Details { get; private set; }
        public Money? DiscountedPrice { get; private set; }
        public Money? AlternativeUnitPrice { get; private set; }

        private Product(
            string name,
            string description,
            Category category,
            string subcategory,
            Photo image,
            Money price,
            ProductDetails details,
            string? id)
        {
            Id = id ?? ObjectId.GenerateNewId().ToString();
            Name = name;
            Description = description;
            Category = category;
            Subcategory = subcategory;
            Image = image;
            Price = price;
            Details = details;
            DiscountedPrice = null;
            AlternativeUnitPrice = Details.IsWeightSwitchAllowed ?
                PricingService.CalculatePrice(Price, Details, Price.Unit.AlternativeUnit()) :
                null;
        }

        public static Product Create(
            string name,
            string description,
            string category,
            string subcategory,
            string image,
            decimal priceAmount,
            string currencyCode,
            string unitCode,
            bool isWeightSwitchAllowed,
            decimal? singleWeight,
            string? id = null)
        {
            var price = new Money(priceAmount, Currency.FromCode(currencyCode), Unit.FromCode(unitCode));

            if (isWeightSwitchAllowed && singleWeight is null)
            {
                throw new ApplicationException("Single weight must be specified when pcs is allowed");
            }

            var details = new ProductDetails(true, false, isWeightSwitchAllowed, singleWeight);

            return new(
                name,
                description,
                Category.FromValue(category),
                subcategory,
                new(image),
                price,
                details,
                id);
        }

        public void StartDiscount(decimal newPriceAmount)
        {
            if (newPriceAmount >= Price.Amount)
            {
                throw new ApplicationException("Discount price must be lower than regular price");
            }

            Details.StartDiscount();
            var newPrice = Price with { Amount = newPriceAmount };

            RaiseDomainEvent(new ProductDiscountStartDomainEvent(Id, newPrice));

            DiscountedPrice = newPrice;
        }

        public void EndDiscount()
        {
            Details.EndDiscount();

            RaiseDomainEvent(new ProductDiscountEndDomainEvent(Id, Price));

            DiscountedPrice = null;
        }

        public void OutOfStock()
        {
            Details.Unavailable();

            RaiseDomainEvent(new ProductOutOfStockDomainEvent(Id));
        }

        public void InStock()
        {
            Details.Available();

            RaiseDomainEvent(new ProductInStockDomainEvent(Id));
        }

        public void Update(UpdateValues newValues)
        {
            Name = newValues.Name;
            Description = newValues.Description;
            Subcategory = newValues.Subcategory;
            Category = Category.FromValue(newValues.Category);
            Image = new(newValues.ImageUrl);

            if (newValues.SingleWeight.HasValue && newValues.IsWeightSwitchAllowed)
            {
                Details.AllowWeightSwitch(newValues.SingleWeight.Value, Price.Unit!.AlternativeUnit());
                AlternativeUnitPrice = PricingService.CalculatePrice(Price, Details, Price.Unit.AlternativeUnit());
            }
            else
            {
                Details.ForbidWeightSwitch();
            }

            if (newValues.IsAvailable)
                Details.Available();
            else
                Details.Unavailable();
        }

        public static Product Create(CreateValues requestValues, string? id = null)
        {
            if (requestValues.Price is null ||
                string.IsNullOrWhiteSpace(requestValues.Image))
            {
                throw new ApplicationException("Image and price is required");
            }

            return Create(
                requestValues.Name,
                requestValues.Description,
                requestValues.Category,
                requestValues.Subcategory,
                requestValues.Image,
                requestValues.Price.Amount,
                requestValues.Price.Currency.Code,
                requestValues.Price.Unit.Code,
                requestValues.IsWeightSwitchAllowed,
                requestValues.SingleWeight,
                id);
        }
    }
}