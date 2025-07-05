using CSharpFunctionalExtensions;
using Familia.Domain.Shared;

namespace Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects
{
    public record HelpRequisites
    {
        private const int MAX_PAYMENT_LENGTH = 30;
        private const int MAX_DETAILS_LENGTH = 1000;

        private HelpRequisites(string paymentMethod, string details)
        {
            PaymentMethod = paymentMethod;
            Details = details;
        }
        public string PaymentMethod { get; }
        public string Details { get; }

        public static Result<HelpRequisites, Error> Create(string paymentMethod, string details)
        {
            if (string.IsNullOrWhiteSpace(paymentMethod) || paymentMethod.Length > MAX_PAYMENT_LENGTH)
                Errors.General.ValueIsInvalid("Метод оплаты");

            if (string.IsNullOrWhiteSpace(details) || paymentMethod.Length > MAX_DETAILS_LENGTH)
                Errors.General.ValueIsInvalid("Детали");

            return new HelpRequisites(paymentMethod, details);
        }
    }
}
