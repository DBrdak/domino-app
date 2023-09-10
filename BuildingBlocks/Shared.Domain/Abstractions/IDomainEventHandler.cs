﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Shared.Domain.Abstractions
{
    internal interface IDomainEventHandler : INotificationHandler<>
    {
    }
}