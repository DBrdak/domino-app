using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.Domain.PriceLists.Events;
using Shared.Domain.Exceptions;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.Tests.PriceLists;

public class AggregateLineItemWithProductTests
{
    [Fact]
    public void AggregateLineItemWithProduct_ValidData_ShouldAggregate()
    {
        // Arrange
        var retailPriceList = TestPriceLists.RetailPriceList;
        var lineItem = new LineItem("Test Line Item", new (10.9m, Currency.Pln, Unit.Kg));
        var productId = "productId";
        retailPriceList.AddLineItem(lineItem);

        // Act
        retailPriceList.AggregateLineItemWithProduct(lineItem.Name, productId);
        
        // Assert
        Assert.True(retailPriceList.LineItems.First().ProductId == productId);
    }
    
    [Fact]
    public void AggregateLineItemWithProduct_InvalidContractorData_ShouldThrow()
    {
        // Arrange
        var businessPriceList = TestPriceLists.BusinessPriceList;
        var lineItem = new LineItem("Test Line Item", new (10.9m, Currency.Pln, Unit.Kg));
        var productId = "productId";
        businessPriceList.AddLineItem(lineItem);

        // Act
        var aggregateLineItemWithProductFunc = () => businessPriceList.AggregateLineItemWithProduct(lineItem.Name, productId);
        
        // Assert
        Assert.Throws<DomainException<PriceList>>(aggregateLineItemWithProductFunc);
        Assert.True(businessPriceList.LineItems.First().ProductId is null);
    }
    
    [Fact]
    public void AggregateLineItemWithProduct_InvalidProductData_ShouldThrow()
    {
        // Arrange
        var retailPriceList = TestPriceLists.RetailPriceList;
        var lineItem = new LineItem("Test Line Item", new (10.9m, Currency.Pln, Unit.Kg));
        var productId = "productId";
        var productId2 = "productId2";
        retailPriceList.AddLineItem(lineItem);
        retailPriceList.AggregateLineItemWithProduct(lineItem.Name, productId);

        // Act
        var aggregateLineItemWithProductFunc = () => retailPriceList.AggregateLineItemWithProduct(lineItem.Name, productId2);
        
        // Assert
        Assert.Throws<DomainException<PriceList>>(aggregateLineItemWithProductFunc);
        Assert.True(retailPriceList.LineItems.First().ProductId == productId);
    }
}