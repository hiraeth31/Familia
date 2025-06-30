using CSharpFunctionalExtensions;

namespace Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects
{
    public record FullName
    {
        private FullName(string firstName, string lastName, string patronymic)
        {
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
        }
        public string FirstName { get; }
        public string LastName { get; }
        public string Patronymic { get; }

        public static Result<FullName> Create(string firstName, string lastName, string patronymic)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Result.Failure<FullName>("Имя обязательно к заполнению!");

            if (string.IsNullOrWhiteSpace(lastName))
                return Result.Failure<FullName>("Фамилия обязательно к заполнению!");

            if (string.IsNullOrWhiteSpace(patronymic))
                return Result.Failure<FullName>("Отчество обязательно к заполнению!");

            return Result.Success(new FullName(firstName, lastName, patronymic));
        }
    }
}
