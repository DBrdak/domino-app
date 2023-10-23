using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shops.Domain.Shared
{
    public sealed record Seller(string FirstName, string LastName, [RegularExpression("^\\d{9}$")]string? PhoneNumber);
}