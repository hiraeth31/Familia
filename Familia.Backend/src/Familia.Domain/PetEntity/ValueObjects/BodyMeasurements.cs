using CSharpFunctionalExtensions;

namespace Familia.Domain.PetEntity.ValueObjects
{
    public record BodyMeasurements
    {
        private BodyMeasurements(double weight, double height)
        {
            Weight = weight;
            Height = height;
        }
        public double Weight { get; }
        public double Height { get; }

        public static Result<BodyMeasurements> Create(double weight, double height)
        {
            if (weight <= 0 && weight > 120)
                return Result.Failure<BodyMeasurements>("Недопустимое значение веса!");

            if (height <= 0 && height > 200)
                return Result.Failure<BodyMeasurements>("Недопустимое значение роста!");

            return Result.Success<BodyMeasurements>(new BodyMeasurements(weight, height));
        }
    }
}