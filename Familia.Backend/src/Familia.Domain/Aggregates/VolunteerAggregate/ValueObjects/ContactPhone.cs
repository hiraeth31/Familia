using CSharpFunctionalExtensions;
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

        public static Result<ContactPhone> Create(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return Result.Failure<ContactPhone>("Номер телефона обязателен к заполнению!");

            if (!PhoneRegex.IsMatch(phone))
                return Result.Failure<ContactPhone>("Неверный формат номера телефона!");

            return Result.Success(new ContactPhone(phone));
        }
    }
}
