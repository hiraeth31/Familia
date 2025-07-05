using CSharpFunctionalExtensions;
using Familia.Domain.Shared;

namespace Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects
{
    public record BodyMeasurements
    {
        private const int MAX_WEIGHT = 125;
        private const int MAX_HEIGHT = 200;
        private BodyMeasurements(double weight, double height)
        {
            Weight = weight;
            Height = height;
        }
        public double Weight { get; }
        public double Height { get; }

        public static Result<BodyMeasurements, Error> Create(double weight, double height)
        {
            if (weight <= 0 && weight > MAX_WEIGHT)
                return Errors.General.ValueIsInvalid("Вес");

            if (height <= 0 && height > MAX_HEIGHT)
                return Errors.General.ValueIsInvalid("Рост");

            return new BodyMeasurements(weight, height);
        }
    }
}