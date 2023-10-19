using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Shared.Domain.DateTimeRange
{
    public sealed record DateTimeRange
    {
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime Start { get; private set; }
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime End { get; private set; }

        [JsonConstructor]
        public DateTimeRange(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new ApplicationException("End date precedes start date");
            }

            Start = start;
            End = end;
        }

        public DateTimeRange(DateTime day, TimeRange timeRange)
        {
            Start = new DateTime(
                day.Year,
                day.Month,
                day.Day,
                timeRange.Start.Hour,
                timeRange.Start.Minute,
                timeRange.Start.Second);
            End = new DateTime(
                day.Year,
                day.Month,
                day.Day,
                timeRange.End.Hour,
                timeRange.End.Minute,
                timeRange.End.Second);
        }

        public DateTimeRange(DateOnly day, TimeRange timeRange)
        {
            Start = new DateTime(
                day.Year,
                day.Month,
                day.Day,
                timeRange.Start.Hour,
                timeRange.Start.Minute,
                timeRange.Start.Second);
            End = new DateTime(
                day.Year,
                day.Month,
                day.Day,
                timeRange.End.Hour,
                timeRange.End.Minute,
                timeRange.End.Second);
        }

        public DateTimeRange ParseToUTC()
        {
            Start = Start.ToUniversalTime();
            End = End.ToUniversalTime();

            return this;
        }
    }
}