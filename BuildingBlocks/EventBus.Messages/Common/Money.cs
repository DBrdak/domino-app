using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Common
{
    public record Money(decimal Amount, string Currency, string Unit);
}