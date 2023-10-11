using System.Text.Json.Serialization;
using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;

namespace Shops.Domain.Shared
{
    public sealed record ShopWorkingDay
    {
        public WeekDay WeekDay { get; init; }
        public TimeRange? OpenHours { get; private set; }
        public TimeRange? CachedOpenHours { get; private set; }
        public bool IsClosed { get; private set; }

        public ShopWorkingDay(string weekDay, TimeRange openHours)
        {
            WeekDay = WeekDay.FromValue(weekDay);
            OpenHours = openHours;
            IsClosed = false;
        }

        [JsonConstructor]
        public ShopWorkingDay(WeekDay weekDay, TimeRange openHours)
        {
            WeekDay = weekDay;
            OpenHours = openHours;
            IsClosed = false;
        }

        internal ShopWorkingDay(string weekDay)
        {
            WeekDay = WeekDay.FromValue(weekDay);
            OpenHours = null;
            IsClosed = true;
        }

        internal ShopWorkingDay(WeekDay weekDay)
        {
            WeekDay = weekDay;
            OpenHours = null;
            IsClosed = true;
        }

        public void UpdateOpenHours(TimeRange openHours)
        {
            if (IsClosed)
            {
                Open();
            }

            OpenHours = openHours;
        }

        internal void Close()
        {
            if (IsClosed)
            {
                return;
            }

            IsClosed = true;
            CachedOpenHours = OpenHours;
            OpenHours = null;
        }

        internal void Open()
        {
            if (!IsClosed)
            {
                return;
            }

            IsClosed = false;
            OpenHours = CachedOpenHours;
            CachedOpenHours = null;
        }
    }
}