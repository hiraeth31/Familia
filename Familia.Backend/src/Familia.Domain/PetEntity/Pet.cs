using CSharpFunctionalExtensions;
using Familia.Domain.PetEntity.ValueObjects;
using Familia.Domain.Shared;
using Familia.Domain.SpeciesEntity;
using Familia.Domain.VolunteerEntity;
using Familia.Domain.VolunteerEntity.ValueObjects;

namespace Familia.Domain.PetEntity
{
    public class Pet: IdEntity<PetId>
    {
        //ef core navigation
        public Volunteer Volunteer { get; private set; } = null!;

        //ef core
        private Pet(PetId id): base(id)
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
            Name = name;
            Description = description;
            Color = color;
            HealthInfo = healthInfo;
            Address = address;
            BodyMeasurements = bodyMeasurements;
            ContactPhone contactPhone1 = contactPhone;
            HelpRequisites = helpRequisites;
            IsNeutered = isNeutered;
            Birthday = birthday;
            IsVaccinated = isVaccinated;
            HelpStatus = helpStatus;
            CreationDate = creationDate;
        }
        public SpeciesBreed SpeciesBreed { get; private set; }
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public string Color { get; private set; } = default!;
        public string HealthInfo { get; private set; } = default!;
        public Address Address { get; private set; }
        public BodyMeasurements BodyMeasurements { get; private set; }
        public ContactPhone PhoneNumber { get; private set; }
        public HelpRequisites HelpRequisites { get; private set; }
        public bool IsNeutered { get; private set; } = default!;
        public DateTime Birthday { get; private set; } = default!;
        public bool IsVaccinated { get; private set; } = default!;
        public HelpStatus HelpStatus { get; private set; }
        public DateTime CreationDate { get; private set; } = default!;

        public static Result<Pet> Create(PetId petId,
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
                return Result.Failure<Pet>("Имя обязательно к заполнению!");

            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<Pet>("Описание обязательно к заполнению!");

            if (string.IsNullOrWhiteSpace(color))
                return Result.Failure<Pet>("Цвет обязателен к заполнению!");

            if (string.IsNullOrWhiteSpace(healthInfo))
                return Result.Failure<Pet>("Информация о здоровье обязательна к заполнению!");

            if (address is null)
                return Result.Failure<Pet>("Адрес обязателен к заполнению!");

            if (bodyMeasurements is null)
                return Result.Failure<Pet>("Параметры тела обязательны к заполнению!");

            if (contactPhone is null)
                return Result.Failure<Pet>("Телефон обязателен к заполнению!");

            if (helpStatus is null)
                return Result.Failure<Pet>("Статус помощи обязателен к заполнению!");


            var pet = new Pet(petId, speciesBreed, name, description, color, healthInfo, 
                address, bodyMeasurements, contactPhone, helpRequisites, isNeutered, 
                birthday, isVaccinated, helpStatus, creationDate);

            return Result.Success(pet);
        }
    }
}
