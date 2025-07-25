using CSharpFunctionalExtensions;
using Familia.Domain.Aggregates.VolunteerAggregate.Entities;
using Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects;
using Familia.Domain.Shared;
using Familia.Domain.Shared.EntityIds;
using Familia.Domain.Shared.Extenstions;

namespace Familia.Domain.Aggregates.VolunteerAggregate.AggregateRoot
{
    public sealed class Volunteer: IdEntity<VolunteerId>, ISoftDeletable
    {
        private bool _isDeleted = false;
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
            HelpRequisites helpRequisites,
            List<SocialMedia> socialMedias)
            : base(volunteerId)
        {
            FullName = fullName;
            Email = email;
            Description = description;
            YearsOfExperience = yearsOfExperience;
            ContactPhone = contactPhone;
            HelpRequisites = helpRequisites;
            _socialMedias = new List<SocialMedia>(socialMedias);
        }
        public FullName FullName { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public int YearsOfExperience { get; private set; } = default!;
        public int NumberOfAnimalsFoundHome => FoundHomeAnimalsNumber();
        public int NumberOfAnimalsLookingHome => LookingForHomeAnimalsNumber();
        public int NumberOfAnimalsNeedsHelp => NeedHelpAnimalsNumber();
        public ContactPhone ContactPhone { get; private set; } = default!;
        public HelpRequisites HelpRequisites { get; private set; } = default!;
        public IReadOnlyList<SocialMedia> SocialMedias => _socialMedias;
        public IReadOnlyList<Pet> Pets => _pets;

        public static Result<Volunteer, Error> Create(VolunteerId volunteerId,
            FullName fullName,
            string email,
            string description,
            int yearsOfExperience,
            ContactPhone contactPhone,
            HelpRequisites helpRequisites,
            List<SocialMedia> socialMedias)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Errors.General.ValueIsInvalid("Электронный адрес");

            if (string.IsNullOrWhiteSpace(description))
                return Errors.General.ValueIsInvalid("Описание");

            var result = new Volunteer(volunteerId, fullName, email, description, yearsOfExperience,
                contactPhone, helpRequisites, socialMedias);

            return result;
        }
        private int FoundHomeAnimalsNumber() => _pets.Count(p => p.HelpStatus == HelpStatus.FoundHome);
        private int LookingForHomeAnimalsNumber() => _pets.Count(p => p.HelpStatus == HelpStatus.LookingForHome);
        private int NeedHelpAnimalsNumber() => _pets.Count(p => p.HelpStatus == HelpStatus.NeedsHelp);

        public void UpdateMainInfo
            (FullName fullName,
            string email,
            string description,
            int yearsOfExperience,
            ContactPhone contactPhone)
        {
            FullName = fullName;
            Email = email;
            Description = description;
            YearsOfExperience = yearsOfExperience;
            ContactPhone = contactPhone;
        }

        public void UpdateSocialMedia
            (IEnumerable<SocialMedia> socialMedia)
        {
            _socialMedias.Clear();
            _socialMedias.AddRange(socialMedia);
        }

        public void UpdateHelpRequisite
            (HelpRequisites helpRequisites)
        {
            HelpRequisites = helpRequisites;
        }

        public void Delete()
        {
            if (_isDeleted) return;

            _isDeleted = true;
        }
        public void Restore()
        {
            if (!_isDeleted) return;

            _isDeleted = false;
        }
    }
}
