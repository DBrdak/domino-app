using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Date;
using Shared.Domain.Exceptions;

namespace Shared.Domain.Tests.DateTimeRange.DateTimeRange
{
    public class ConstructorTests
    {
        [Fact]
        public void DateTimeConstructor_ShouldReturnValidDateTimeRange()
        {
            // Arrange
            var start = DateTimeService.UtcNow;
            var end = DateTimeService.UtcNow.AddHours(5);

            // Act
            var dateTimeRange = new Domain.DateTimeRange.DateTimeRange(start, end);

            // Assert
            Assert.Equal(start, dateTimeRange.Start);
            Assert.Equal(end, dateTimeRange.End);
        }

        [Fact]
        public void DateTimeConstructor_InvalidDateTime_ShouldThrow()
        {
            // Arrange
            var start = DateTimeService.UtcNow;
            var end = DateTimeService.UtcNow.AddHours(-5);

            // Act
            var dateTimeRangeCreateFunc = () => new Domain.DateTimeRange.DateTimeRange(start, end);

            // Assert
            Assert.Throws<DomainException<Domain.DateTimeRange.DateTimeRange>>(dateTimeRangeCreateFunc);
        }

        [Fact]
        public void DateOnlyWithTimeRangeConstructor_ShouldReturnValidDateTimeRange()
        {
            // Arrange
            var day = DateTimeService.Today;
            var timeRange = new Domain.DateTimeRange.TimeRange("5:00", "12:00");

            // Act
            var dateTimeRange = new Domain.DateTimeRange.DateTimeRange(day, timeRange);

            // Assert
            Assert.Equal(day, DateOnly.FromDateTime(dateTimeRange.Start));
            Assert.Equal(day, DateOnly.FromDateTime(dateTimeRange.End));
            Assert.Equal(timeRange.Start, TimeOnly.FromDateTime(dateTimeRange.Start));
            Assert.Equal(timeRange.End, TimeOnly.FromDateTime(dateTimeRange.End));
        }
    }
}
