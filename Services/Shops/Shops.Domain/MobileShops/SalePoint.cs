using System.Text.Json.Serialization;
using MongoDB.Bson;
using Shared.Domain.Abstractions.Entities;
using Shared.Domain.Date;
using Shared.Domain.DateTimeRange;
using Shared.Domain.Location;

namespace Shops.Domain.MobileShops
{
    public sealed class SalePoint : Entity
    {
        public Location Location { get; init; }
        public WeekDay WeekDay { get; private set; }
        public bool IsClosed { get; private set; }
        public TimeRange? OpenHours { get; private set; }
        public TimeRange? CachedOpenHours { get; private set; }

        [JsonConstructor]
        public SalePoint(Location location, WeekDay weekDay, bool isClosed, TimeRange? openHours, TimeRange? cachedOpenHours, string id) : base(id)
        {
            Location = location;
            WeekDay = weekDay;
            IsClosed = isClosed;
            OpenHours = openHours;
            CachedOpenHours = cachedOpenHours;
        }

        public SalePoint(Location location, TimeRange openHours, WeekDay weekDay) : base(ObjectId.GenerateNewId().ToString())
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

        internal void Update(SalePoint updatedSalePoint)
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