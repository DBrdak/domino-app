﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Domain.DateTimeRange
{
    public sealed record TimeRange
    {
        [JsonConverter(typeof(TimeOnlyJsonConverter))]
        public TimeOnly Start { get; init; }
        [JsonConverter(typeof(TimeOnlyJsonConverter))]
        public TimeOnly End { get; init; }

        public TimeRange(TimeOnly start, TimeOnly end)
        {
            if (start > end)
            {
                throw new ApplicationException("End time precedes start time");
            }

            Start = start;
            End = end;
        }
    }
}