using CSharpFunctionalExtensions;
using Familia.Domain.Shared;
using System.Text.RegularExpressions;

namespace Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects
{
    public record ContactPhone
    {
        private static readonly Regex PhoneRegex = new(
            @"^(\+7|8)?[\s\-]?\(?\d{3}\)?[\s\-]?\d{3}[\s\-]?\d{2}[\s\-]?\d{2}$",
            RegexOptions.Compiled);
        private ContactPhone(string phone)
        {
            Phone = phone;
        }
        public string Phone { get; }

        public static Result<ContactPhone, Error> Create(string input)
        {
            var number = input.Trim();

            if (!PhoneRegex.IsMatch(number))
                return Errors.General.ValueIsInvalid("Номер телефона");

            return new ContactPhone(number);
        }
    }
}
