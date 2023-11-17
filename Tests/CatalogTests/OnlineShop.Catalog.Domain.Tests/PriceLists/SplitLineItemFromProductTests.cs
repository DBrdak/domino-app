using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Exceptions;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.Tests.PriceLists;

public class SplitLineItemFromProductTests
{
    [Fact]
    public void SplitLineItemFromProduct_ValidData_ShouldSplit()
    {
        // Arrange
        var retailPriceList = TestPriceLists.RetailPriceList();
        var lineItem = new LineItem("Test Line Item", new (10.9m, Currency.Pln, Unit.Kg));
        var productId = "productId";
        retailPriceList.AddLineItem(lineItem);
        retailPriceList.AggregateLineItemWithProduct(lineItem.Name, productId);

        // Act
        retailPriceList.SplitLineItemFromProduct(productId);
        
        // Assert
        Assert.True(retailPriceList.LineItems.First().ProductId is null);
    }
    
    [Fact]
    public void SplitLineItemFromProduct_InvalidData_ShouldNotSplitAndThrow()
    {
        // Arrange
        var retailPriceList = TestPriceLists.RetailPriceList();
        var lineItem = new LineItem("Test Line Item", new (10.9m, Currency.Pln, Unit.Kg));
        var productId = "productId";
        var productId2 = "productId2";
        retailPriceList.AddLineItem(lineItem);
        retailPriceList.AggregateLineItemWithProduct(lineItem.Name, productId);

        // Act
        var splitLineItemFromProductFunc = () => retailPriceList.SplitLineItemFromProduct(productId2);
        
        // Assert
        Assert.Throws<DomainException<PriceList>>(splitLineItemFromProductFunc);
        Assert.True(retailPriceList.LineItems.First().ProductId == productId);
    }
}