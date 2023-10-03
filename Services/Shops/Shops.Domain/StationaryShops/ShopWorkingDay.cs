using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shops.Domain.StationaryShops
{
    public sealed record ShopWorkingDay
    {
        public WeekDay WeekDay { get; init; }
        public TimeRange? OpenHours { get; private set; }
        [JsonInclude]
        private TimeRange? _cachedOpenHours;
        public bool IsClosed { get; private set; }

        public ShopWorkingDay(string weekDay, TimeRange openHours)
        {
            WeekDay = WeekDay.FromCode(weekDay);
            OpenHours = openHours;
            IsClosed = false;
        }

        public ShopWorkingDay(WeekDay weekDay, TimeRange openHours)
        {
            WeekDay = weekDay;
            OpenHours = openHours;
            IsClosed = false;
        }

        internal ShopWorkingDay(string weekDay)
        {
            WeekDay = WeekDay.FromCode(weekDay);
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
            IsClosed = true;
            _cachedOpenHours = OpenHours;
            OpenHours = null;
        }

        internal void Open()
        {
            IsClosed = false;
            OpenHours = _cachedOpenHours;
            _cachedOpenHours = null;
        }
    }
}