using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using OnlineShop.Catalog.Domain.Common;
using OnlineShop.Catalog.Domain.Events;
using Shared.Domain.Abstractions;
using Shared.Domain.Money;
using Shared.Domain.Photo;

namespace OnlineShop.Catalog.Domain
{
    public sealed class Product : Entity
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public Category Category { get; init; }
        public string Subcategory { get; init; }
        public Photo Image { get; init; }
        public Money Price { get; init; }
        public ProductDetails Details { get; init; }
        public Money? DiscountedPrice { get; private set; }
        public Money? AlternativeUnitPrice { get; init; }

        private Product(
            string name,
            string description,
            Category category,
            string subcategory,
            Photo image,
            Money price,
            ProductDetails details) : base(ObjectId.GenerateNewId().ToString())
        {
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
            decimal? singleWeight)
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
                details);
        }

        public void StartDiscount(decimal newPriceAmount)
        {
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
    }
}