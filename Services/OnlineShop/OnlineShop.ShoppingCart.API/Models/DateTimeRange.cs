using Newtonsoft.Json;

namespace OnlineShop.ShoppingCart.API.Models
{
    public class DateTimeRange
    {
        public DateTime Start { get; init; }
        public DateTime End { get; init; }

        [JsonConstructor]
        public DateTimeRange(DateTime start, DateTime end)
        {
            Start = start.ToUniversalTime();
            End = end.ToUniversalTime();
        }
    }
}