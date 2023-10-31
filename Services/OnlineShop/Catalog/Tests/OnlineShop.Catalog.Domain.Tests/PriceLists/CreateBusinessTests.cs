using OnlineShop.Catalog.Domain.PriceLists;
using OnlineShop.Catalog.Domain.Shared;
using Shared.Domain.Exceptions;

namespace OnlineShop.Catalog.Domain.Tests.PriceLists;

public class CreateBusinessTests
{
    [Fact]
    public void BusinessPriceListCreate_ValidData_ShouldReturnPriceList()
    {
        // Arrange
        
        // Act
        var businessPriceList = PriceList.CreateBusiness("Test 1", "Test Contractor", Category.Meat);

        // Assert
        Assert.NotNull(businessPriceList);
    }
    
    [Theory]
    [InlineData("test1", null)]
    [InlineData("test2", " ")]
    public void BusinessPriceListCreate_InvalidContractorData_ShouldThrow(string priceListName, string contractorName)
    {
        // Arrange
        
        // Act
        var businessPriceListCreateFunc = () => PriceList.CreateBusiness(priceListName, contractorName, Category.Meat);

        // Assert
        Assert.Throws<DomainException<Contractor>>(businessPriceListCreateFunc);
    }
    
    [Theory]
    [InlineData(null, "test 1")]
    [InlineData(" ", "test 2")]
    public void BusinessPriceListCreate_InvalidNameData_ShouldThrow(string priceListName, string contractorName)
    {
        // Arrange
        
        // Act
        var businessPriceListCreateFunc = () => PriceList.CreateBusiness(priceListName, contractorName, Category.Meat);

        // Assert
        Assert.Throws<DomainException<PriceList>>(businessPriceListCreateFunc);
    }
}