using System.ComponentModel.DataAnnotations;

namespace Shops.Domain.Shared
{
    public sealed record Seller(string FirstName, string LastName, [RegularExpression("^\\d{9}$")]string? PhoneNumber);
}