using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.Domain.Shared;
using Shared.Domain.Exceptions;

namespace OnlineShop.Catalog.Domain.Tests.PriceLists;

public class CreateRetailTests
{
    [Fact]
    public void RetailPriceListCreate_ValidData_ShouldReturnPriceList()
    {
        // Arrange
        
        // Act
        var retailPriceList = PriceList.CreateRetail("Test 1", Category.Meat);

        // Assert
        Assert.NotNull(retailPriceList);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void RetailPriceListCreate_InvalidData_ShouldThrow(string priceListName)
    {
        // Arrange
        
        // Act
        var retailPriceListCreateFunc = () => PriceList.CreateRetail(priceListName, Category.Meat);

        // Assert
        Assert.Throws<DomainException<PriceList>>(retailPriceListCreateFunc);
    }
}