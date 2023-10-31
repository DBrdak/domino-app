using OnlineShop.Catalog.Domain.Products;
using Shared.Domain.Exceptions;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.Tests.Products;

public class ProductUpdateTests
{
    [Theory]
    [ClassData(typeof(ProductUpdateSuccessTestData))]
    public void ProductUpdate_ValidData_ShouldUpdateProduct(UpdateValues values)
    {
        // Arragnge
        var product = new ProductFactory().CreateProduct();
        var oldProduct = product.Clone();
        
        // Act
        product.Update(values);

        // Assert
        Assert.NotEqual(oldProduct, product);
        Assert.NotEqual(oldProduct.Description, product.Description);
        Assert.Equal(oldProduct.Name, product.Name);
        Assert.Equal(oldProduct.Price, product.Price);
        Assert.Equal(oldProduct.Id, product.Id);
        Assert.Equal(oldProduct.Category, product.Category);
    }
    
    [Theory]
    [ClassData(typeof(ProductUpdateFailureTestData))]
    public void ProductUpdate_InvalidData_ShouldNotUpdate(UpdateValues values)
    {
        // Arragnge
        var product = new ProductFactory().CreateProduct();
        var oldProduct = product.Clone();
        
        // Act
        var productUpdateFunc = () => product.Update(values);

        // Assert
        Assert.Equivalent(oldProduct, product);
    }
}