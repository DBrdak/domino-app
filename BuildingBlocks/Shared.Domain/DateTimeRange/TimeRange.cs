using Shared.Domain.Exceptions;
using System.Text.Json.Serialization;

namespace Shared.Domain.DateTimeRange
{
    public sealed record TimeRange
    {
        [JsonConverter(typeof(TimeOnlyJsonConverter))]
        public TimeOnly Start { get; init; }
        [JsonConverter(typeof(TimeOnlyJsonConverter))]
        public TimeOnly End { get; init; }

        [JsonConstructor]
        public TimeRange(TimeOnly start, TimeOnly end)
        {
            if (start > end)
            {
                throw new DomainException<TimeRange>("End time precedes start time");
            }

            Start = start;
            End = end;
        }

        public TimeRange(DateTime start, DateTime end) : this(
            new TimeOnly(start.Hour, start.Minute),
            new TimeOnly(end.Hour, end.Minute))
        { }

        public TimeRange(DateTimeRange dateRange) : this(dateRange.Start, dateRange.End)
        { }
    }
}