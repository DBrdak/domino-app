using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Common
{
    public record Location(string Name, string Latitude, string Longitude);
}