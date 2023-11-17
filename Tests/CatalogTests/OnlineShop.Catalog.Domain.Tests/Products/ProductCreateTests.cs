using OnlineShop.Catalog.Domain.Products;
using OnlineShop.Catalog.Domain.Shared;
using Shared.Domain.Exceptions;
using Shared.Domain.Money;

namespace OnlineShop.Catalog.Domain.Tests.Products
{
    public class ProductCreateTests
    {
        [Theory]
        [ClassData(typeof(ProductCreateSuccessTestData))]
        public void Create_ProductWithValidData_ReturnsProductInstance(CreateValues createValues, Category category, string image, Money price)
        {
            // Arrange
            createValues.AttachCategory(category);
            createValues.AttachImage(image);
            createValues.AttachPrice(price);

            // Act
            Product product = Product.Create(createValues);

            // Assert
            Assert.NotNull(product);
        }
        
        [Theory]
        [ClassData(typeof(ProductCreateFailureTestData))]
        public void Create_ProductWithInValidData_ThrowsError(CreateValues createValues, Category category, string image, Money price)
        {
            // Arrange
            createValues.AttachCategory(category);
            createValues.AttachImage(image);
            createValues.AttachPrice(price);

            // Act
            var productCreateFunc = () => Product.Create(createValues);

            // Assert
            Assert.Throws<DomainException<Product>>(productCreateFunc);
        }
    }
}