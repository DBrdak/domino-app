using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Exceptions;

namespace Shared.Domain.Tests.Money.Currency
{
    public class FromCodeTests
    {
        [Theory]
        [InlineData("PLN")]
        [InlineData("pln")]
        [InlineData("ZŁ")]
        [InlineData("zł")]
        public void FromCode_ValidData_ShouldCreateCurrencyInstance(string code)
        {
            // Arrange

            // Act
            var currency = Domain.Money.Currency.FromCode(code);

            // Assert
            Assert.Equal(currency, Domain.Money.Currency.Pln);
        }

        [Theory]
        [InlineData("zl")]
        [InlineData("")]
        public void FromCode_InvalidData_ShouldThrow(string code)
        {
            // Arrange

            // Act
            var currencyCreateFunc = () => Domain.Money.Currency.FromCode(code);

            // Assert
            Assert.Throws<DomainException<Domain.Money.Currency>>(currencyCreateFunc);
        }
    }
}
