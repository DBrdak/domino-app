using System.Text.Json.Serialization;
using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;

namespace Shops.Domain.MobileShops
{
    public sealed record SalePoint(Location Location, TimeRange? OpenHours, WeekDay WeekDay)
    {
        public bool IsClosed { get; private set; }
        public TimeRange? OpenHours { get; private set; }
        [JsonInclude]
        private TimeRange? _cachedOpenHours;

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