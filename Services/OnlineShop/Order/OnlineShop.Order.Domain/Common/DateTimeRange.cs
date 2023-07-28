namespace OnlineShop.Order.Domain.Common
{
    public class DateTimeRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public DateTimeRange(DateTime start, DateTime end)
        {
            Start = start.ToUniversalTime();
            End = end.ToUniversalTime();
        }
    }
}