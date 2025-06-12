using CSharpFunctionalExtensions;
using Familia.Domain.PetEntity.ValueObjects;
using Familia.Domain.Shared;
using Familia.Domain.SpeciesEntity;

namespace Familia.Domain.PetEntity
{
    public class Pet: IdEntity<PetId>
    {
        //ef core
        private Pet(PetId id): base(id)
        {
        }
        public Pet(PetId petId, string name): base(petId)
        {
            Name = name;
        }
        public SpeciesBreed SpeciesBreed { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public string Color { get; private set; } = default!;
        public string HealthInfo { get; private set; } = default!;
        public Address Address { get; private set; } = default!;
        public double Weight {  get; private set; } = default!;
        public double Height { get; private set; } = default;
        public ContactPhone PhoneNumber { get; private set; } = default!;
        public HelpRequisites HelpRequisites { get; private set; } = default!;
        public bool IsNeutered { get; private set; } = default!;
        public DateOnly Birthday { get; private set; } = default!;
        public bool IsVaccinated { get; private set; } = default!;
        public HelpStatus HelpStatus { get; private set; } = default!;
        public DateTime CreationDate { get; private set; } = default!;

        public static Result<Pet> Create(PetId id,string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Pet>("Title can't be empty!");


            var pet = new Pet(id, name);

            return Result.Success(pet);
        }
    }
}
