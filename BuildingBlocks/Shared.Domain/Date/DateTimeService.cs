using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Date
{
    /// <summary>
    /// <b>Use it instead of DateTime struct to define new date</b>
    /// </summary>
    public sealed class DateTimeService
    {
        public static DateTime UtcNow => DateTime.UtcNow;
    }
}