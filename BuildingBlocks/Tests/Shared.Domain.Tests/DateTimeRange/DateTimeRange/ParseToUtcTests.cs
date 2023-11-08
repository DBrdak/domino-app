using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Date;

namespace Shared.Domain.Tests.DateTimeRange.DateTimeRange
{
    public class ParseToUtcTests
    {
        [Fact]
        public void ParseToUtc_ShouldChangeDateTimeRangeDateTimesToUtcKind()
        {
            // Arrange
            var unspecifiedDateTimeRange = new Domain.DateTimeRange.DateTimeRange(
                DateTimeService.UtcNow,
                DateTimeService.UtcNow.AddHours(10));

            // Act
            var utcDateTimeRange = unspecifiedDateTimeRange.ParseToUTC();

            // Assert
            Assert.Equal(DateTimeKind.Utc, utcDateTimeRange.Start.Kind);
            Assert.Equal(DateTimeKind.Utc, utcDateTimeRange.End.Kind);
        }
    }
}
