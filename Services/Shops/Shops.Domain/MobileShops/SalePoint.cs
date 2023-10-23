using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;

namespace Shops.Domain.MobileShops
{
    public sealed record SalePoint
    {
        public Location Location { get; init; }
        public WeekDay WeekDay { get; private set; }
        public bool IsClosed { get; private set; }
        public TimeRange? OpenHours { get; private set; }
        public TimeRange? CachedOpenHours { get; private set; }

        public SalePoint(Location location, TimeRange openHours, WeekDay weekDay)
        {
            Location = location;
            OpenHours = openHours;
            WeekDay = weekDay;
            IsClosed = false;
            CachedOpenHours = null;
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

        public void Update(SalePoint updatedSalePoint)
        {
            if (IsClosed)
            {
                Open();
            }

            OpenHours = updatedSalePoint.OpenHours;
            WeekDay = updatedSalePoint.WeekDay;
        }
    }
}