using CSharpFunctionalExtensions;
using Familia.Domain.Shared;

namespace Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects
{
    public record Position
    {
        public static Position First => new(1);
        public int Value { get; }

        public Position(int value)
        {
            Value = value;
        }

        public Result<Position, Error> Forward()
            => Create(Value + 1);

        public Result<Position, Error> Back()
            => Create(Value - 1);

        public static Result<Position, Error> Create(int position)
        {
            if (position < 1)
                return Errors.General.ValueIsInvalid("Position");

            return new Position(position);
        }
    }
}
