using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.Domain.Shared;
using Shared.Domain.Exceptions;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.Tests.PriceLists;

public class AddLineItemTests
{
    [Theory]
    [ClassData(typeof(AddLineItemTestData))]
    public void AddLineItem_ValidData_ShouldAddLineItemToPriceList(string lineItemName, Money lineItemPrice)
    {
        // Arrange
        var retailPriceList = TestPriceLists.RetailPriceList();
        var businessPriceList = TestPriceLists.BusinessPriceList();
        var lineItem = new LineItem(lineItemName, lineItemPrice);
        
        // Act
        retailPriceList.AddLineItem(lineItem);
        businessPriceList.AddLineItem(lineItem);
        
        // Assert
        Assert.Contains(retailPriceList.LineItems, li => ReferenceEquals(li, lineItem));
        Assert.Contains(businessPriceList.LineItems, li => ReferenceEquals(li, lineItem));
    }
    
    [Theory]
    [ClassData(typeof(AddLineItemTestData))]
    public void AddLineItem_InvalidData_ShouldThrow(string lineItemName, Money lineItemPrice)
    {
        // Arrange
        var retailPriceList = PriceList.CreateRetail("Retail Test", Category.Meat);
        var businessPriceList = PriceList.CreateBusiness("Business Test", "Contractor Test", Category.Meat);
        var lineItem = new LineItem(lineItemName, lineItemPrice);
        var lineItem2 = new LineItem(lineItemName, lineItemPrice + 5);
        
        // Act
        retailPriceList.AddLineItem(lineItem);
        businessPriceList.AddLineItem(lineItem);
        var retailPriceListAddLineItemFunc2 = () => retailPriceList.AddLineItem(lineItem2);
        var businessPriceListAddLineItemFunc2 = () => businessPriceList.AddLineItem(lineItem2);
        
        // Assert
        Assert.Throws<DomainException<PriceList>>(retailPriceListAddLineItemFunc2);
        Assert.Throws<DomainException<PriceList>>(businessPriceListAddLineItemFunc2);
        Assert.Contains(retailPriceList.LineItems, li => ReferenceEquals(li, lineItem));
        Assert.Contains(businessPriceList.LineItems, li => ReferenceEquals(li, lineItem));
        Assert.DoesNotContain(retailPriceList.LineItems, li => ReferenceEquals(li, lineItem2));
        Assert.DoesNotContain(businessPriceList.LineItems, li => ReferenceEquals(li, lineItem2));
    }
}