using CSharpFunctionalExtensions;
using Familia.Domain.Shared;

namespace Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects
{
    public record FullName
    {
        private const int MAX_LENGTH = 25;
        private FullName(string firstName, string lastName, string patronymic)
        {
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
        }
        public string FirstName { get; }
        public string LastName { get; }
        public string Patronymic { get; }

        public static Result<FullName, Error> Create(string firstName, string lastName, string patronymic)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length > MAX_LENGTH)
                return Errors.General.ValueIsInvalid("Имя");

            if (string.IsNullOrWhiteSpace(lastName) || firstName.Length > MAX_LENGTH)
                return Errors.General.ValueIsInvalid("Фамилия");

            if (string.IsNullOrWhiteSpace(patronymic) || firstName.Length > MAX_LENGTH)
                return Errors.General.ValueIsInvalid("Отчество");

            return new FullName(firstName, lastName, patronymic);
        }
    }
}
