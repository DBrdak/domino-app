using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Exceptions;

namespace Shared.Domain.Tests.Money.Money
{
    public class ConstructorTests
    {
        [Theory]
        [ClassData(typeof(MoneyConstructorValidTestData))]
        public void MoneyConstructor_ValidData_ShouldCreateMoneyInstance(decimal amount, Domain.Money.Currency currency, Domain.Money.Unit? unit)
        {
            // Arrange

            // Act
            var money = new Domain.Money.Money(amount, currency, unit);

            // Assert
            Assert.Equal(money.Currency, currency);
            Assert.Equal(money.Unit, unit);
            Assert.Equal(money.Amount, amount);
        }

        [Fact]
        public void MoneyConstructor_InvalidData_ShouldThrow()
        {
            // Arrange
            var amount = -5;
            var currency = Domain.Money.Currency.Pln;

            // Act
            var moneyCreateFunc = () => new Domain.Money.Money(amount, currency);

            // Assert
            Assert.Throws<DomainException<Domain.Money.Money>>(moneyCreateFunc);
        }
    }
}
