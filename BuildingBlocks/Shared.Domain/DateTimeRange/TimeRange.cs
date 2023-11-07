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

        public TimeRange(string startString, string endString)
        {
            var parsingResult = new bool[] { false, false };

            parsingResult[0] = TimeOnly.TryParse(startString, out var start);
            parsingResult[1] = TimeOnly.TryParse(endString, out var end);

            if (!parsingResult.All(r => r))
            {
                throw new DomainException<TimeRange>("Passed invalid time format");
            }

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

        public override string ToString() => $"{Start.ToShortTimeString()} - {End.ToShortTimeString()}";
    }
}