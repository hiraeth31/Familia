using CSharpFunctionalExtensions;
using Familia.Domain.Shared;

namespace Familia.Domain.SpeciesEntity
{
    public class Breed: IdEntity<BreedId>
    {
        //ef core
        private Breed(BreedId id): base(id)
        {
        }
        public Breed(BreedId breedId, string name): base(breedId)
        {
            Name = name;
        }

        public string Name { get; private set; } = default!;

        public static Result<Breed> Create(BreedId breedId, string name) 
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Breed>("Порода обязательна к заполению");

            return Result.Success<Breed>(new Breed(breedId, name));
        }
    }
}
