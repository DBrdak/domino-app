using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.Domain.PriceLists.Events;
using Shared.Domain.Exceptions;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.Tests.PriceLists;

public class UpdateLineItemPriceTests
{
    [Theory]
    [ClassData(typeof(UpdateLineItemSuccessTestData))]
    public void UpdateLineItem_ValidData_ShouldUpdateLineItem(string lineItemName, Money newPrice)
    {
        // Arrange
        var retailPriceList = TestPriceLists.RetailPriceList;
        var businessPriceList = TestPriceLists.BusinessPriceList;
        var lineItem = new LineItem("Test Line Item", new (10.9m, Currency.Pln, Unit.Kg));
        retailPriceList.AddLineItem(lineItem);
        businessPriceList.AddLineItem(lineItem);
        
        // Act
        retailPriceList.UpdateLineItemPrice(lineItemName, newPrice);
        businessPriceList.UpdateLineItemPrice(lineItemName, newPrice);
        
        // Assert
        Assert.True(retailPriceList.LineItems.First().Price == newPrice);
        Assert.True(businessPriceList.LineItems.First().Price == newPrice);
    }
    
    [Theory]
    [ClassData(typeof(UpdateLineItemFailureTestData))]
    public void UpdateLineItem_InvalidData_ShouldNotUpdateLineItem(string lineItemName, Money newPrice)
    {
        // Arrange
        var retailPriceList = TestPriceLists.RetailPriceList;
        var businessPriceList = TestPriceLists.BusinessPriceList;
        var lineItem = new LineItem("Test Line Item", new (10.9m, Currency.Pln, Unit.Kg));
        retailPriceList.AddLineItem(lineItem);
        businessPriceList.AddLineItem(lineItem);
        
        // Act
        var updateRetailLineItemPriceFunc = () => retailPriceList.UpdateLineItemPrice(lineItemName, newPrice);
        var updateBusinessLineItemPriceFunc = () => businessPriceList.UpdateLineItemPrice(lineItemName, newPrice);
        
        // Assert
        Assert.Throws<DomainException<PriceList>>(updateRetailLineItemPriceFunc);
        Assert.Throws<DomainException<PriceList>>(updateBusinessLineItemPriceFunc);
        Assert.True(retailPriceList.LineItems.First().Price == lineItem.Price);
        Assert.True(businessPriceList.LineItems.First().Price == lineItem.Price);
    }
    
    [Theory]
    [ClassData(typeof(UpdateLineItemSuccessTestData))]
    public void UpdateRetailLineItemWithAggregatedProduct_ValidData_ShouldUpdateLineItemAndRaiseDomainEvent(string lineItemName, Money newPrice)
    {
        // Arrange
        var retailPriceList = TestPriceLists.RetailPriceList;
        var lineItem = new LineItem("Test Line Item", new (10.9m, Currency.Pln, Unit.Kg));
        retailPriceList.AddLineItem(lineItem);
        retailPriceList.AggregateLineItemWithProduct(lineItem.Name, "exampleId");
        
        // Act
        retailPriceList.UpdateLineItemPrice(lineItemName, newPrice);
        
        // Assert
        Assert.True(retailPriceList.LineItems.First().Price == newPrice);
        Assert.True(retailPriceList.GetDomainEvents().First() is LineItemPriceUpdatedDomainEvent);
    }
}