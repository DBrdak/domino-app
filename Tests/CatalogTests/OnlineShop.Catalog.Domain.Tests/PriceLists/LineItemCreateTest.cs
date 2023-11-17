using OnlineShop.Catalog.Domain.PriceLists;
using Shared.Domain.Exceptions;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.Tests.PriceLists;

public class LineItemCreateTest
{
    [Theory]
    [ClassData(typeof(CreateLineItemSuccessTestData))]
    public void NewLineItem_ValidData_ShouldCreateLineItem(string lineItemName, Money lineItemPrice)
    {
        // Arrange
        
        // Act
        var lineItem = new LineItem(lineItemName, lineItemPrice);

        // Assert
        Assert.NotNull(lineItem);
    }
    
    [Theory]
    [ClassData(typeof(CreateLineItemFailureTestData))]
    public void NewLineItem_InvalidData_ShouldNotCreateLineItem(string lineItemName, Money lineItemPrice)
    {
        // Arrange
        
        // Act
        var newLineItemFunc = () => new LineItem(lineItemName, lineItemPrice);

        // Assert
        Assert.Throws<DomainException<LineItem>>(newLineItemFunc);
    }
}