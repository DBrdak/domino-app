using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.Domain.PriceLists.Events;
using Shared.Domain.Exceptions;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.Tests.PriceLists;

public class DeleteLineItemTests
{
    [Theory]
    [InlineData("Test Line Item")]
    public void DeleteLineItem_ValidData_ShouldDeleteLineItem(string lineItemName)
    {
        // Arrange
        var retailPriceList = TestPriceLists.RetailPriceList;
        var businessPriceList = TestPriceLists.BusinessPriceList;
        var lineItem = new LineItem("Test Line Item", new (10.9m, Currency.Pln, Unit.Kg));
        retailPriceList.AddLineItem(lineItem);
        businessPriceList.AddLineItem(lineItem);
        
        // Act
        retailPriceList.DeleteLineItem(lineItemName);
        businessPriceList.DeleteLineItem(lineItemName);
        
        // Assert
        Assert.Empty(retailPriceList.LineItems);
        Assert.Empty(businessPriceList.LineItems);
    }
    
    [Theory]
    [InlineData("")]
    public void DeleteLineItem_InvalidData_ShouldNotDeleteLineItem(string lineItemName)
    {
        // Arrange
        var retailPriceList = TestPriceLists.RetailPriceList;
        var businessPriceList = TestPriceLists.BusinessPriceList;
        var lineItem = new LineItem("Test Line Item", new (10.9m, Currency.Pln, Unit.Kg));
        retailPriceList.AddLineItem(lineItem);
        businessPriceList.AddLineItem(lineItem);
        
        // Act
        var deleteRetailLineItemFunc = () => retailPriceList.DeleteLineItem(lineItemName);
        var deleteBusinessLineItemFunc = () => businessPriceList.DeleteLineItem(lineItemName);
        
        // Assert
        Assert.Throws<DomainException<PriceList>>(deleteRetailLineItemFunc);
        Assert.Throws<DomainException<PriceList>>(deleteBusinessLineItemFunc);
        Assert.NotEmpty(retailPriceList.LineItems);
        Assert.NotEmpty(businessPriceList.LineItems);
    }
    
    [Theory]
    [InlineData("Test Line Item")]
    public void DeleteRetailLineItemWithAggregatedProduct_ValidData_ShouldDeleteLineItemAndRaiseDomainEvent(string lineItemName)
    {
        // Arrange
        var retailPriceList = TestPriceLists.RetailPriceList;
        var lineItem = new LineItem("Test Line Item", new (10.9m, Currency.Pln, Unit.Kg));
        retailPriceList.AddLineItem(lineItem);
        retailPriceList.AggregateLineItemWithProduct(lineItem.Name, "exampleId");
        
        // Act
        retailPriceList.DeleteLineItem(lineItemName);
        
        // Assert
        Assert.Empty(retailPriceList.LineItems);
        Assert.True(retailPriceList.GetDomainEvents().First() is LineItemDeletedDomainEvent);
    }
}