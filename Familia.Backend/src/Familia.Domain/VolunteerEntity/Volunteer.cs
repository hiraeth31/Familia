using CSharpFunctionalExtensions;
using Familia.Domain.PetEntity;
using Familia.Domain.PetEntity.ValueObjects;
using Familia.Domain.Shared;
using Familia.Domain.VolunteerEntity.ValueObjects;

namespace Familia.Domain.VolunteerEntity
{
    public class Volunteer: IdEntity<VolunteerId>
    {
        private readonly List<Pet> _pets = [];
        private readonly List<SocialMedia> _socialMedias = [];
        //ef core
        private Volunteer(VolunteerId id): base(id)
        {
        }
        public Volunteer(VolunteerId volunteerId,
            FullName fullName,
            string email,
            string description,
            int yearsOfExperience,
            ContactPhone contactPhone,
            HelpRequisites helpRequisites)
            : base(volunteerId)
        {
            Email = email;
            Description = description;
            YearsOfExperience = yearsOfExperience;
            ContactPhone = contactPhone;
            HelpRequisites = helpRequisites;
        }
        public FullName FullName { get; private set; }
        public string Email { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public int YearsOfExperience { get; private set; } = default!;
        public int NumberOfAnimalsFoundHome => FoundHomeAnimalsNumber();
        public int NumberOfAnimalsLookingHome => LookingForHomeAnimalsNumber();
        public int NumberOfAnimalsNeedsHelp => NeedHelpAnimalsNumber();
        public ContactPhone ContactPhone { get; private set; }
        public HelpRequisites HelpRequisites { get; private set; }
        public IReadOnlyList<SocialMedia> SocialMedias => _socialMedias;
        public IReadOnlyList<Pet> Pets => _pets;

        public static Result<Volunteer> Create(VolunteerId volunteerId,
            FullName fullName,
            string email,
            string description,
            int yearsOfExperience,
            ContactPhone contactPhone,
            HelpRequisites helpRequisites)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure<Volunteer>("Электронный адрес обязателен к заполнению!");

            if (fullName is null)
                return Result.Failure<Volunteer>("ФИО обязателено к заполнению!");

            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<Volunteer>("Описание обязателено к заполнению!");

            if (contactPhone is null)
                return Result.Failure<Volunteer>("Телефон обязателен к заполнению!");

            if (helpRequisites is null)
                return Result.Failure<Volunteer>("Реквизиты обязателены к заполнению!");

            var result = new Volunteer(volunteerId, fullName,email, description, yearsOfExperience,
                contactPhone, helpRequisites);

            return Result.Success<Volunteer>(result);
        }
        private int FoundHomeAnimalsNumber() => _pets.Where(p => p.HelpStatus == HelpStatus.FoundHome).Count();
        private int LookingForHomeAnimalsNumber() => _pets.Where(p => p.HelpStatus == HelpStatus.LookingForHome).Count();
        private int NeedHelpAnimalsNumber() => _pets.Where(p => p.HelpStatus == HelpStatus.NeedsHelp).Count();

    }
}
