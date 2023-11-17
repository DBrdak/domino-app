using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Exceptions;

namespace Shared.Domain.Tests.Money.Money
{
    public class FromStringTests
    {
        [Theory]
        [InlineData("25,8 PLN/kg")]
        [InlineData("25.8 ZŁ/KG")]
        [InlineData("25.8 zł/kg")]
        [InlineData("25.8 pln/szt")]
        [InlineData("25.8 zł/szt")]
        [InlineData("25.8 zł")]
        [InlineData("25.8 pln")]
        [InlineData("25,8PLN/kg")]
        public void FromString_ValidString_ShouldReturnMoneyInstance(string stringValue)
        {
            // Arrange

            // Act
            var money = Domain.Money.Money.FromString(stringValue);

            // Assert
            Assert.NotNull(money);
            Assert.IsType<Domain.Money.Money>(money);
        }

        [Theory]
        [InlineData("25,8 PLN kg")]
        [InlineData("-25.8 ZŁ/KG")]
        [InlineData("25.8 zł/kilogram")]
        [InlineData("25.8 pln/sztukę")]
        [InlineData("")]
        [InlineData("5")]
        public void FromString_InvalidString_ShouldThrow(string stringValue)
        {
            // Arrange

            // Act
            var moneyCreateFunc = () => Domain.Money.Money.FromString(stringValue);

            // Assert
            Assert.Throws<DomainException<Domain.Money.Money>>(moneyCreateFunc);
        }
    }
}
