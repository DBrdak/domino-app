using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.Exceptions;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.Tests.Products;

public class ProductUpdatePriceTests
{
    [Theory]
    [ClassData(typeof(ProductPriceUpdateSuccessTestData))]
    public void ProductPriceUpdate_ValidData_ShouldUpdateProductPrice(Money newPrice)
    {
        // Arragnge
        var product = new ProductFactory().CreateProduct();
        var oldAltPrice = product.AlternativeUnitPrice;
        
        // Act
        product.UpdatePrice(newPrice);

        // Assert
        Assert.Equal(newPrice, product.Price);
        Assert.NotEqual(oldAltPrice, product.AlternativeUnitPrice);
    }
    
    [Theory]
    [ClassData(typeof(ProductPriceUpdateFailureTestData))]
    public void ProductPriceUpdate_InvalidData_ShouldThrowError(Money newPrice)
    {
        // Arragnge
        var product = new ProductFactory().CreateProduct();
        var oldPrice = product.Price;
        var oldAltPrice = product.AlternativeUnitPrice;

        // Act
        var productUpdateFunc = () => product.UpdatePrice(newPrice);

        // Assert
        Assert.Throws<DomainException<Product>>(productUpdateFunc);
        Assert.Equal(product.Price, oldPrice);
        Assert.Equal(oldAltPrice, product.AlternativeUnitPrice);
    }
}