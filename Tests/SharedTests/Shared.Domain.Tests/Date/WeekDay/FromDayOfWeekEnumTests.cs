using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Tests.Date.WeekDay
{
    public class FromDayOfWeekEnumTests
    {
        [Theory]
        [InlineData(0, "Niedziela")]
        [InlineData(1, "Poniedziałek")]
        [InlineData(2, "Wtorek")]
        [InlineData(3, "Środa")]
        [InlineData(4, "Czwartek")]
        [InlineData(5, "Piątek")]
        [InlineData(6, "Sobota")]
        public void FromDayOfWeekIndex_ShouldReturnWeekDay(int weekDayIndex, string weekDayName)
        {
            // Arrange

            // Act
            var weekDay = Domain.Date.WeekDay.FromDayOfWeekEnum(weekDayIndex);

            // Assert
            Assert.Equal(weekDay.Value, weekDayName);
        }

        [Theory]
        [InlineData(DayOfWeek.Sunday, "Niedziela")]
        [InlineData(DayOfWeek.Monday, "Poniedziałek")]
        [InlineData(DayOfWeek.Tuesday, "Wtorek")]
        [InlineData(DayOfWeek.Wednesday, "Środa")]
        [InlineData(DayOfWeek.Thursday, "Czwartek")]
        [InlineData(DayOfWeek.Friday, "Piątek")]
        [InlineData(DayOfWeek.Saturday, "Sobota")]
        public void FromDayOfWeekEnum_ShouldReturnWeekDay(DayOfWeek weekDayEnum, string weekDayName)
        {
            // Arrange
            
            // Act
            var weekDay = Domain.Date.WeekDay.FromDayOfWeekEnum(weekDayEnum);

            // Assert
            Assert.Equal(weekDay.Value, weekDayName);
        }
    }
}
