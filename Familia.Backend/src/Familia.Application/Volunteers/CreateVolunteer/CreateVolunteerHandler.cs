using CSharpFunctionalExtensions;
using Familia.Domain.Aggregates.VolunteerAggregate.AggregateRoot;
using Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects;
using Familia.Domain.Shared;
using Familia.Domain.Shared.EntityIds;

namespace Familia.Application.Volunteers.CreateVolunteer
{
    public class CreateVolunteerHandler
    {
        private readonly IVolunteersRepository _volunteersRepository;

        public CreateVolunteerHandler(IVolunteersRepository volunteersRepository)
        {
            _volunteersRepository = volunteersRepository;
        }

        public async Task<Result<Guid, Error>> Handle(
            CreateVolunteerRequest request, CancellationToken cancellationToken = default)
        {
            var fullNameResult = FullName.Create(request.FullName.FirstName, request.FullName.LastName, request.FullName.Patronymic);
            if (fullNameResult.IsFailure)
                return fullNameResult.Error;

            var numberResult = ContactPhone.Create(request.Number);
            if (numberResult.IsFailure)
                return numberResult.Error;

            var existingNumberVolunteer = await _volunteersRepository.GetByNumber(numberResult.Value);
            if (existingNumberVolunteer.IsSuccess)
                return Errors.General.AlreadyExist();

            var helpRequisituiesResult = HelpRequisites.Create(request.HelpRequisities.PaymentMethod, request.HelpRequisities.Details);
            if (helpRequisituiesResult.IsFailure)
                return helpRequisituiesResult.Error;

            var socialMediaResult = request.SocialMedias
                .Select(s => SocialMedia.Create(s.Name, s.Link).Value).ToList();
            //if (socialMediaResult.Any(s => s.IsFailure))
            //    return Errors.General.ValueIsInvalid("Social media");

            var volunteerId = VolunteerId.NewVolunteerId();

            var volunteerResult = Volunteer.Create(
                volunteerId,
                fullNameResult.Value,
                request.Email,
                request.Description,
                request.YearsOfExperience,
                numberResult.Value,
                helpRequisituiesResult.Value,
                socialMediaResult);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            await _volunteersRepository.Add(volunteerResult.Value, cancellationToken);

            return (Guid)volunteerResult.Value.Id;
        }
    }
}
