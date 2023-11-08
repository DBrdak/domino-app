using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Date;
using Shared.Domain.Exceptions;

namespace Shared.Domain.Tests.DateTimeRange.TimeRange
{
    public class ConstructorTests
    {
        [Fact]
        public void TimeOnlyConstructor_ValidData_ShouldReturnTimeRange()
        {
            // Arrange
            var start = TimeOnly.Parse("5:00");
            var end = TimeOnly.Parse("15:00");

            // Act
            var timeRange = new Domain.DateTimeRange.TimeRange(start, end);

            // Assert
            Assert.Equal(start, timeRange.Start);
            Assert.Equal(end, timeRange.End);
        }

        [Fact]
        public void TimeOnlyConstructor_InvalidData_ShouldThrow()
        {
            // Arrange
            var start = TimeOnly.Parse("15:00");
            var end = TimeOnly.Parse("5:00");

            // Act
            var timeRangeCreateFunc = () => new Domain.DateTimeRange.TimeRange(start, end);

            // Assert
            Assert.Throws<DomainException<Domain.DateTimeRange.TimeRange>>(timeRangeCreateFunc);
        }

        [Fact]
        public void StringConstructor_ValidData_ShouldReturnTimeRange()
        {
            // Arrange
            var start = "5:00";
            var end = "15:00";

            // Act
            var timeRange = new Domain.DateTimeRange.TimeRange(start, end);

            // Assert
            Assert.Equal(TimeOnly.Parse(start), timeRange.Start);
            Assert.Equal(TimeOnly.Parse(end), timeRange.End);
        }

        [Fact]
        public void StringConstructor_InvalidDataFormat_ShouldThrow()
        {
            // Arrange
            var start = "15,00";
            var end = "5,00";

            // Act
            var timeRangeCreateFunc = () => new Domain.DateTimeRange.TimeRange(start, end);

            // Assert
            Assert.Throws<DomainException<Domain.DateTimeRange.TimeRange>>(timeRangeCreateFunc);
        }

        [Fact]
        public void StringConstructor_InvalidDataRange_ShouldThrow()
        {
            // Arrange
            var start = "15:00";
            var end = "5:00";

            // Act
            var timeRangeCreateFunc = () => new Domain.DateTimeRange.TimeRange(start, end);

            // Assert
            Assert.Throws<DomainException<Domain.DateTimeRange.TimeRange>>(timeRangeCreateFunc);
        }

        [Fact]
        public void DateTimeConstructor_ValidData_ShouldReturnTimeRange()
        {
            // Arrange
            var start = DateTimeService.UtcNow;
            var end = DateTimeService.UtcNow.AddSeconds(2);

            // Act
            var timeRange = new Domain.DateTimeRange.TimeRange(start, end);

            // Assert
            Assert.Equal(TimeOnly.FromDateTime(start).ToString("t"), timeRange.Start.ToString("t"));
            Assert.Equal(TimeOnly.FromDateTime(end).ToString("t"), timeRange.End.ToString("t"));
        }

        [Fact]
        public void DateTimeConstructor_InvalidDataRange_ShouldThrow()
        {
            // Arrange
            var start = DateTimeService.UtcNow;
            var end = DateTimeService.UtcNow.AddMinutes(-1);

            // Act
            var timeRangeCreateFunc = () => new Domain.DateTimeRange.TimeRange(start, end);

            // Assert
            Assert.Throws<DomainException<Domain.DateTimeRange.TimeRange>>(timeRangeCreateFunc);
        }
    }
}
