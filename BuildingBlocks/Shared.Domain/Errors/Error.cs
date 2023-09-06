using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Errors
{
    public record Error(string Code, string Name)
    {
        public static Error None = new(string.Empty, string.Empty);

        public static Error NullValue = new("Error.NullValue", "Null value was provided");

        public static Error InvalidRequest(string name) => new("Error.InvalidRequest", name);

        public static Error TaskFailed(string name) => new("Error.TaskFailed", name);
    }
}