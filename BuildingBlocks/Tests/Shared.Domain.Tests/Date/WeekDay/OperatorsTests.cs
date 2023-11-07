using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Tests.Date.WeekDay
{
    public class OperatorsTests
    {
        [Theory]
        [InlineData("Poniedziałek", "Wtorek")]
        [InlineData("Wtorek", "Środa")]
        [InlineData("Środa", "Czwartek")]
        [InlineData("Czwartek", "Piątek")]
        [InlineData("Piątek", "Sobota")]
        [InlineData("Sobota", "Niedziela")]
        [InlineData("Niedziela", "Poniedziałek")]
        public void IncrementOperator_ShouldReturnNextWeekDay(string weekDayValue, string expectedNextWeekDayValue)
        {
            // Arrange
            var weekDay = Domain.Date.WeekDay.FromValue(weekDayValue);
            var expectedNextWeekDay = Domain.Date.WeekDay.FromValue(expectedNextWeekDayValue);

            // Act
            weekDay++;

            // Assert
            Assert.Equal(weekDay, expectedNextWeekDay);
        }

        [Theory]
        [InlineData("Poniedziałek", "Niedziela")]
        [InlineData("Wtorek", "Poniedziałek")]
        [InlineData("Środa", "Wtorek")]
        [InlineData("Czwartek", "Środa")]
        [InlineData("Piątek", "Czwartek")]
        [InlineData("Sobota", "Piątek")]
        [InlineData("Niedziela", "Sobota")]
        public void DecrementOperator_ShouldReturnPreviousWeekDay(string weekDayValue, string expectedPreviousWeekDayValue)
        {
            // Arrange
            var weekDay = Domain.Date.WeekDay.FromValue(weekDayValue);
            var expectedPreviousWeekDay = Domain.Date.WeekDay.FromValue(expectedPreviousWeekDayValue);

            // Act
            weekDay--;

            // Assert
            Assert.Equal(weekDay, expectedPreviousWeekDay);
        }
    }
}
