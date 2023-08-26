using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.DateTimeRange
{
    public sealed record DateTimeRange
    {
        public DateTime Start { get; init; }
        public DateTime End { get; init; }

        public DateTimeRange(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new ApplicationException("End date precedes start date");
            }

            Start = start;
            End = end;
        }
    }
}