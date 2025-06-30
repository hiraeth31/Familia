using CSharpFunctionalExtensions;

namespace Familia.Domain.Shared.ValueObjects
{
    public record Address
    {
        private Address(string country, string city, string street, string house)
        {
            Country = country;
            City = city;
            Street = street;
            House = house;
        }
        public string Country { get; }
        public string City { get; }
        public string Street { get; }
        public string House { get; }

        public static Result<Address> Create(string country, string city, string street, string house)
        {
            if (string.IsNullOrWhiteSpace(country))
                return Result.Failure<Address>("Адрес обязателен к заполнению!");

            if (string.IsNullOrWhiteSpace(city))
                return Result.Failure<Address>("Город обязателен к заполнению!");

            if (string.IsNullOrWhiteSpace(street))
                return Result.Failure<Address>("Улица обязательна к заполнению!");

            if (string.IsNullOrWhiteSpace(house))
                return Result.Failure<Address>("Номер дома обязателен к заполнению!");

            return Result.Success(new Address(country, city, street, house));
        }
    }
}
