using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Shared.Domain.Exceptions;

namespace Shops.Domain.Shared
{
    public sealed record Seller
    {
        private const string phoneNumberRegex = "^\\d{9}$";

        public Seller(string firstName, string lastName, string? phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                throw new DomainException<Seller>("First and last name of seller is required");
            }

            if (phoneNumber is not null && !Regex.IsMatch(phoneNumber, phoneNumberRegex))
            {
                throw new DomainException<Seller>("Invalid phone number");
            }

            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
        }

        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string? PhoneNumber { get; init; }

        public void Deconstruct(out string firstName, out string lastName, out string? phoneNumber)
        {
            firstName = this.FirstName;
            lastName = this.LastName;
            phoneNumber = this.PhoneNumber;
        }
    }
}