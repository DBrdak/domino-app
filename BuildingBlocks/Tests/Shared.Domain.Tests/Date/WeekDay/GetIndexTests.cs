using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Tests.Date.WeekDay
{
    public class GetIndexTests
    {
        [Theory]
        [InlineData("Poniedziałek",0)]
        [InlineData("Wtorek",1)]
        [InlineData("Środa",2)]
        [InlineData("Czwartek",3)]
        [InlineData("Piątek",4)]
        [InlineData("Sobota",5)]
        [InlineData("Niedziela",6)]
        public void GetIndex_ShouldReturnValidWeekDayIndex(string weekDayValue, int expectedIndex)
        {
            // Arrange
            var weekDay = Domain.Date.WeekDay.FromValue(weekDayValue);

            // Act
            var index = Domain.Date.WeekDay.GetIndex(weekDay);

            // Assert
            Assert.Equal(index, expectedIndex);
        }
    }
}
