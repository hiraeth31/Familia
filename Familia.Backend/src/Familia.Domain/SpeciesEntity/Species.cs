using CSharpFunctionalExtensions;
using Familia.Domain.Shared;

namespace Familia.Domain.SpeciesEntity
{
    public class Species: IdEntity<SpeciesId>
    {
        private readonly List<Breed> _breeds = [];
        //ef core
        private Species(SpeciesId id): base(id)
        {
        }
        public Species(SpeciesId speciesId, string name): base(speciesId)
        {
            Name = name;
        }
        public string Name { get; private set; } = default!;
        public IReadOnlyList<Breed> Breeds => _breeds;

        public static Result<Species> Create(SpeciesId speciesId, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Species>("Вид обязателен к заполнению!");

            return Result.Success<Species>(new Species(speciesId, name));
        }
    }
}
