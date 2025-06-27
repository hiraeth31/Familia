using CSharpFunctionalExtensions;
using Familia.Domain.BreedEntity;

namespace Familia.Domain.SpeciesEntity
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
            return Result.Success<SpeciesBreed>(new SpeciesBreed(breedId, speciesId));
        }
    }
}
