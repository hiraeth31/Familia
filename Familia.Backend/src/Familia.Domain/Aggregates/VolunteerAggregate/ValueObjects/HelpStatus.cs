using CSharpFunctionalExtensions;

namespace Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects
{
    public record HelpStatus
    {
        public static readonly HelpStatus NeedsHelp = new("Нуждается в помощи");
        public static readonly HelpStatus LookingForHome = new("Ищет дом");
        public static readonly HelpStatus FoundHome = new("Нашел дом");
        private static readonly HelpStatus[] _all = [NeedsHelp, LookingForHome, FoundHome];
        private HelpStatus(string status)
        {
            Status = status;
        }
        public string Status { get; }

        public static Result<HelpStatus> Create(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return Result.Failure<HelpStatus>("Статус помощи обязателен к заполнению!");

            var match = _all.FirstOrDefault(h => string.Equals(h.Status, status.Trim(), StringComparison.InvariantCultureIgnoreCase));

            if (match == null)
                return Result.Failure<HelpStatus>("Поле статуса некорректно!");

            return Result.Success(new HelpStatus(status));
        }
    }
}
