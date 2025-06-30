using CSharpFunctionalExtensions;
using Familia.Domain.Shared.EntityIds;

namespace Familia.Domain.Aggregates.SpeciesAggregate
{
    public record SpeciesBreed
    {
        private SpeciesBreed(BreedId breedId, SpeciesId speciesId)
        {
            BreedId = breedId;
            SpeciesId = speciesId;
        }
        public BreedId BreedId { get; }
        public SpeciesId SpeciesId { get; }

        public static Result<SpeciesBreed> Create(BreedId breedId, SpeciesId speciesId)
        {
            return Result.Success(new SpeciesBreed(breedId, speciesId));
        }
    }
}
