namespace Shared.Domain.DateTimeRange
{
    public sealed record DateTimeRange
    {
        public DateTime Start { get; init; }
        public DateTime End { get; init; }

        public DateTimeRange()
        {
            
        }

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
    }
}