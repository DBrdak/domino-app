using Shared.Domain.Exceptions;
using Shared.Domain.Money;

namespace Shared.Domain.Tests.Quantity
{
    public class ConstructorTests
    {
        [Fact]
        public void Constructor_ValidData_ShouldCreateInstanceOfQuantity()
        {
            // Arrange

            // Act
            var quantity = new Domain.Quantity.Quantity(15, Unit.Kg);

            // Assert
            Assert.NotNull(quantity);
        }

        [Fact]
        public void Constructor_ValidData_ShouldThrow()
        {
            // Arrange

            // Act
            var quantityCreateFunc = () => new Domain.Quantity.Quantity(-15, Unit.Kg);

            // Assert
            Assert.Throws<DomainException<Domain.Quantity.Quantity>>(quantityCreateFunc);
        }
    }
}
