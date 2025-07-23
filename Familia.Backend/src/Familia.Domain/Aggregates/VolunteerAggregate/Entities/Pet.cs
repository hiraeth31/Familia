using CSharpFunctionalExtensions;
using Familia.Domain.Aggregates.SpeciesAggregate;
using Familia.Domain.Aggregates.VolunteerAggregate.AggregateRoot;
using Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects;
using Familia.Domain.Shared;
using Familia.Domain.Shared.EntityIds;
using Familia.Domain.Shared.Extenstions;
using Familia.Domain.Shared.ValueObjects;

namespace Familia.Domain.Aggregates.VolunteerAggregate.Entities
{
    public sealed class Pet : IdEntity<PetId>, ISoftDeletable
    {
        private bool _isDeleted = false;

        //ef core navigation
        public Volunteer Volunteer { get; private set; } = null!;

        //ef core
        private Pet(PetId id) : base(id)
        {
        }
        public Pet(PetId petId,
            SpeciesBreed speciesBreed,
            string name,
            string description,
            string color,
            string healthInfo,
            Address address,
            BodyMeasurements bodyMeasurements,
            ContactPhone contactPhone,
            HelpRequisites helpRequisites,
            bool isNeutered,
            DateTime birthday,
            bool isVaccinated,
            HelpStatus helpStatus,
            DateTime creationDate)
            : base(petId)
        {
            SpeciesBreed = speciesBreed;
            Name = name;
            Description = description;
            Color = color;
            HealthInfo = healthInfo;
            Address = address;
            BodyMeasurements = bodyMeasurements;
            PhoneNumber = contactPhone;
            HelpRequisites = helpRequisites;
            IsNeutered = isNeutered;
            Birthday = birthday;
            IsVaccinated = isVaccinated;
            HelpStatus = helpStatus;
            CreationDate = creationDate;
        }
        public SpeciesBreed SpeciesBreed { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public string Color { get; private set; } = default!;
        public string HealthInfo { get; private set; } = default!;
        public Address Address { get; private set; } = default!;
        public BodyMeasurements BodyMeasurements { get; private set; } = default!;
        public ContactPhone PhoneNumber { get; private set; } = default!;
        public HelpRequisites HelpRequisites { get; private set; } = default!;
        public bool IsNeutered { get; private set; } = default!;
        public DateTime Birthday { get; private set; } = default!;
        public bool IsVaccinated { get; private set; } = default!;
        public HelpStatus HelpStatus { get; private set; } = default!;
        public DateTime CreationDate { get; private set; } = default!;

        public static Result<Pet, Error> Create(PetId petId,
            SpeciesBreed speciesBreed,
            string name,
            string description,
            string color,
            string healthInfo,
            Address address,
            BodyMeasurements bodyMeasurements,
            ContactPhone contactPhone,
            HelpRequisites helpRequisites,
            bool isNeutered,
            DateTime birthday,
            bool isVaccinated,
            HelpStatus helpStatus,
            DateTime creationDate)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Errors.General.ValueIsInvalid("Имя");

            if (string.IsNullOrWhiteSpace(description))
                return Errors.General.ValueIsInvalid("Описание");

            if (string.IsNullOrWhiteSpace(color))
                return Errors.General.ValueIsInvalid("Цвет");

            if (string.IsNullOrWhiteSpace(healthInfo))
                return Errors.General.ValueIsInvalid("Информация о здоровье");

            var pet = new Pet(petId, speciesBreed, name, description, color, healthInfo,
                address, bodyMeasurements, contactPhone, helpRequisites, isNeutered,
                birthday, isVaccinated, helpStatus, creationDate);

            return pet;
        }

        public void Delete()
        {
            _isDeleted = true;
        }

        public void Restore()
        {
            _isDeleted = false;
        }
    }
}
