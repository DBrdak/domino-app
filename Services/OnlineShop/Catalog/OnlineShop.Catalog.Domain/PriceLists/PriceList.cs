﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using OnlineShop.Catalog.Domain.PriceLists.Events;
using Shared.Domain.Abstractions;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.PriceLists
{
    public sealed class PriceList : Entity
    {
        public string Name { get; init; }
        public List<LineItem> LineItems { get; init; }
        public Contractor Contractor { get; init; }

        private PriceList()
        {
        }

        private PriceList(string name, Contractor contractor) : base(ObjectId.GenerateNewId().ToString())
        {
            Name = name;
            Contractor = contractor;
            LineItems = new();
        }

        public static PriceList CreateRetail(string name) => new(name, Contractor.Retail);

        public static PriceList CreateBusiness(string name, string contractorName) =>
            new(name, Contractor.Business(contractorName));

        public void AddLineItem(LineItem lineItem)
        {
            var isDuplicate = LineItems.Any(li =>
                li.Name.ToLower() == lineItem.Name.ToLower());

            if (isDuplicate)
            {
                throw new ApplicationException($"Line item named {lineItem.Name} already exists in price list {Name}");
            }

            LineItems.Add(lineItem);
        }

        public void SplitLineItemFromProduct(string productId)
        {
            var lineItemToUpdate = LineItems.SingleOrDefault(li => li.ProductId == productId);

            if (lineItemToUpdate is null)
            {
                throw new ApplicationException($"No line item found matching product with ID: {productId}");
            }

            lineItemToUpdate.SplitFromProduct();
        }

        public void DeleteLineItem(string lineItemName)
        {
            var lineItemToDelete = LineItems.FirstOrDefault(li =>
                li.Name.ToLower() == lineItemName.ToLower());

            if (lineItemToDelete is null)
            {
                throw new ApplicationException($"Given line item do not exist in price list {Name}");
            }

            LineItems.Remove(lineItemToDelete);

            if (Contractor == Contractor.Retail &&
                lineItemToDelete.ProductId is not null)
            {
                RaiseDomainEvent(new LineItemDeletedDomainEvent(lineItemToDelete.ProductId));
            }
        }

        public void UpdateLineItemPrice(string lineItemName, Money price)
        {
            var lineItemToUpdate = LineItems.FirstOrDefault(li =>
                li.Name.ToLower() == lineItemName.ToLower());

            if (lineItemToUpdate is null)
            {
                throw new ApplicationException($"Given line item do not exist in price list {Name}");
            }

            lineItemToUpdate.UpdatePrice(price);

            if (Contractor == Contractor.Retail &&
                lineItemToUpdate.ProductId is not null)
            {
                RaiseDomainEvent(new LineItemPriceUpdatedDomainEvent(lineItemToUpdate.ProductId));
            }
        }

        public void AggregateLineItemWithProduct(string lineItemName, string productId)
        {
            if (Contractor != Contractor.Retail)
            {
                throw new ApplicationException("Cannot aggregate line item with product if price list is not retail");
            }

            var lineItemToUpdate = LineItems.FirstOrDefault(li =>
                li.Name.ToLower() == lineItemName.ToLower());

            if (lineItemToUpdate is null)
            {
                throw new ApplicationException($"Given line item do not exist in price list {Name}");
            }

            if (lineItemToUpdate.ProductId is not null)
            {
                throw new ApplicationException(
                    $"Given line item is already aggregated with product with ID {lineItemToUpdate.ProductId}");
            }

            lineItemToUpdate.AggregateWithProduct(productId);
        }
    }
}