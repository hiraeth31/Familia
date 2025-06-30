using CSharpFunctionalExtensions;

namespace Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects
{
    public record HelpRequisites
    {
        private HelpRequisites(string paymentMethod, string details)
        {
            PaymentMethod = paymentMethod;
            Details = details;
        }
        public string PaymentMethod { get; }
        public string Details { get; }

        public static Result<HelpRequisites> Create(string paymentMethod, string details)
        {
            if (string.IsNullOrWhiteSpace(paymentMethod))
                return Result.Failure<HelpRequisites>("Метод оплаты обязателен к заполнению!");

            if (string.IsNullOrWhiteSpace(details))
                return Result.Failure<HelpRequisites>("Описание к методу оплаты обязателено к заполнению!");

            return Result.Success(new HelpRequisites(paymentMethod, details));
        }
    }
}
