using Shared.Domain.Exceptions;

namespace Shared.Domain.Tests.Date.WeekDay
{
    public class FromValueTests
    {
        [Theory]
        [InlineData("Niedziela")]
        [InlineData("Poniedziałek")]
        [InlineData("Wtorek")]
        [InlineData("Środa")]
        [InlineData("Czwartek")]
        [InlineData("Piątek")]
        [InlineData("Sobota")]
        [InlineData("niedziela")]
        [InlineData("poniedziałek")]
        [InlineData("wtorek")]
        [InlineData("środa")]
        [InlineData("czwartek")]
        [InlineData("piątek")]
        [InlineData("sobota")]
        public void FromValue_ValidValue_ShouldReturnWeekDay(string value)
        {
            // Arrange

            // Act
            var weekDay = Domain.Date.WeekDay.FromValue(value);
            var normalizedValue = value.Replace(value[0].ToString(), value[0].ToString().ToUpper());

            // Assert
            Assert.Equal(weekDay.Value, normalizedValue);
        }

        [Theory]
        [InlineData("Monday")]
        [InlineData("Tuesday")]
        [InlineData("Wednesday")]
        [InlineData("Thursday")]
        [InlineData("Friday")]
        [InlineData("Saturday")]
        [InlineData("Sunday")]
        [InlineData("poniedzialek")]
        [InlineData("sroda")]
        [InlineData("piatek")]
        public void FromValue_InvalidValue_ShouldThrow(string value)
        {
            // Arrange

            // Act
            var weekDayCreateFunc = () => Domain.Date.WeekDay.FromValue(value);

            // Assert
            Assert.Throws<DomainException<Domain.Date.WeekDay>>(weekDayCreateFunc);
        }
    }
}
