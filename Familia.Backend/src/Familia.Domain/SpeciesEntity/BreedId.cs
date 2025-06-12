namespace Familia.Domain.SpeciesEntity
{
    public record BreedId
    {
        private BreedId(Guid value)
        {
            Value = value;
        }
        public Guid Value { get; }
        public static BreedId NewBreedId() => new BreedId(Guid.NewGuid());
        public static BreedId Empty() => new (Guid.Empty);
    }
}