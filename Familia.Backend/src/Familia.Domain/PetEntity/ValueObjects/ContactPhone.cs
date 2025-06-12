using CSharpFunctionalExtensions;

namespace Familia.Domain.PetEntity.ValueObjects
{
    public record ContactPhone
    {
        private ContactPhone(string phone)
        {
            Phone = phone;
        }
        public string Phone { get; }

        public static Result<ContactPhone> Create(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return Result.Failure<ContactPhone>("Номер телефона обязателен к заполнению!");

            return new ContactPhone(phone);
        }
    }
}
