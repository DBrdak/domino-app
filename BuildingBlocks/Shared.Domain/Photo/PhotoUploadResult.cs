﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Photo
{
    public sealed record PhotoUploadResult(string PhotoUrl, string PhotoId);
}