using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Location
{
    public sealed record Location(string Name, string Latitude, string Longitude);
}