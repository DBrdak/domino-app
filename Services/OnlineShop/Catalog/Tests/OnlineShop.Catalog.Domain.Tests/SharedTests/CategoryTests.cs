using OnlineShop.Catalog.Domain.Shared;
using Shared.Domain.Exceptions;

namespace OnlineShop.Catalog.Domain.Tests.CategoryTests;

public class CategoryTests
{
    [Theory]
    [InlineData("Mięso")]
    [InlineData("Wędlina")]
    [InlineData("Sausage")]
    [InlineData("Meat")]
    [InlineData("meat")]
    public void Category_ValidCreateParams_ShouldReturnCategory(string value)
    {
        //Arrange
        
        //Act
        var category = Category.FromValue(value);
        
        //Assert
        Assert.NotNull(category);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("Wedlina")]
    [InlineData("Wędliny")]
    [InlineData("Mieso")]
    public void Category_InvalidCreateParams_ShouldThrowExcpetion(string value)
    {
        //Arrange

        //Act
        var categoryCreateFunc = () => Category.FromValue(value);

        //Assert
        Assert.Throws<DomainException<Category>>(categoryCreateFunc);
    }
}