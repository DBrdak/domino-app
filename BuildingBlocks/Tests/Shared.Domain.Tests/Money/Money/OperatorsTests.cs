using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Exceptions;

namespace Shared.Domain.Tests.Money.Money
{
    public class OperatorsTests
    {
        [Fact]
        public void MoneyAddOperator_ValidData_ShouldSumAmountsAsMoney()
        {
            // Arrange
            var money1 = new Domain.Money.Money(12.5m, Domain.Money.Currency.Pln, Domain.Money.Unit.Kg);
            var money2 = new Domain.Money.Money(32.1m, Domain.Money.Currency.Pln, Domain.Money.Unit.Kg);

            // Act
            var sum = money1 + money2;

            // Assert
            Assert.Equal(sum.Amount, money1.Amount + money2.Amount);
        }

        [Fact]
        public void MoneyAddOperator_InvalidData_ShouldThrow()
        {
            // Arrange
            var money1 = new Domain.Money.Money(12.5m, Domain.Money.Currency.Pln, Domain.Money.Unit.Kg);
            var money2 = new Domain.Money.Money(32.1m, Domain.Money.Currency.Pln, Domain.Money.Unit.Pcs);

            // Act
            var sumFunc = () => money1 + money2;

            // Assert
            Assert.Throws<DomainException<Domain.Money.Money>>(sumFunc);
        }

        [Fact]
        public void MoneySubstractOperator_ValidData_ShouldSubstractAmounts()
        {
            // Arrange
            var money1 = new Domain.Money.Money(12.5m, Domain.Money.Currency.Pln, Domain.Money.Unit.Kg);
            var amount2 = 5;

            // Act
            var sum = money1 - amount2;

            // Assert
            Assert.Equal(sum.Amount, money1.Amount - amount2);
        }

        [Fact]
        public void MoneySubstractOperator_ValidData_ShouldThrow()
        {
            // Arrange
            var money1 = new Domain.Money.Money(12.5m, Domain.Money.Currency.Pln, Domain.Money.Unit.Kg);
            var amount2 = 200;

            // Act
            var sumFunc = () => money1 - amount2;

            // Assert
            Assert.Throws<DomainException<Domain.Money.Money>>(sumFunc);
        }

        [Fact]
        public void MoneyMultiplyOperator_ValidData_ShouldMultiplyAmounts()
        {
            // Arrange
            var money1 = new Domain.Money.Money(12.5m, Domain.Money.Currency.Pln, Domain.Money.Unit.Kg);
            var amount2 = 15;

            // Act
            var sum = money1 * amount2;

            // Assert
            Assert.Equal(sum.Amount, money1.Amount * amount2);
        }

        [Fact]
        public void MoneyDivideOperator_ValidData_ShouldDivideAmounts()
        {
            // Arrange
            var money1 = new Domain.Money.Money(12.5m, Domain.Money.Currency.Pln, Domain.Money.Unit.Kg);
            var amount2 = 15;

            // Act
            var sum = money1 / amount2;

            // Assert
            Assert.Equal(sum.Amount, money1.Amount / amount2);
        }

        [Fact]
        public void MoneyAddOperator_ValidData_ShouldSumAmounts()
        {
            // Arrange
            var money1 = new Domain.Money.Money(12.5m, Domain.Money.Currency.Pln, Domain.Money.Unit.Kg);
            var amount2 = 15;

            // Act
            var sum = money1 + amount2;

            // Assert
            Assert.Equal(sum.Amount, money1.Amount + amount2);
        }
    }
}
