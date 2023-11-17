using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Exceptions;

namespace Shared.Domain.Tests.Money.Unit
{
    public class FromCodeTests
    {
        [Theory]
        [InlineData("KG")]
        [InlineData("Szt")]
        [InlineData("SZT")]
        [InlineData("kg")]
        public void FromCode_ValidData_ShouldCreateUnitInstance(string code)
        {
            // Arrange

            // Act
            var unit = Domain.Money.Unit.FromCode(code);

            // Assert
            Assert.Equal(unit.Code, code.ToLower());
        }

        [Theory]
        [InlineData("kilogramy")]
        [InlineData("")]
        [InlineData("sztuki")]
        public void FromCode_InvalidData_ShouldThrow(string code)
        {
            // Arrange

            // Act
            var unitCreateFunc = () => Domain.Money.Unit.FromCode(code);

            // Assert
            Assert.Throws<DomainException<Domain.Money.Unit>>(unitCreateFunc);
        }
    }
}
