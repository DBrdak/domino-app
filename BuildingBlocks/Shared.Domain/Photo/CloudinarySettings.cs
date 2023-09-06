using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Photo
{
    public sealed record CloudinarySettings(string CloudName, string ApiKey, string ApiSecret);
}