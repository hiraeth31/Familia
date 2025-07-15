using CSharpFunctionalExtensions;
using Familia.Domain.Aggregates.VolunteerAggregate.AggregateRoot;
using Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects;
using Familia.Domain.Shared;
using Familia.Domain.Shared.EntityIds;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Familia.Application.Volunteers.CreateVolunteer
{
    public class CreateVolunteerHandler
    {
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly ILogger<CreateVolunteerHandler> _logger;

        public CreateVolunteerHandler(
            IVolunteersRepository volunteersRepository,
            ILogger<CreateVolunteerHandler> logger)
        {
            _volunteersRepository = volunteersRepository;
            _logger = logger;
        }

        public async Task<Result<Guid, Error>> Handle(
            CreateVolunteerRequest request, CancellationToken cancellationToken = default)
        {
            var fullName = FullName.Create(request.FullName.FirstName, request.FullName.LastName, request.FullName.Patronymic).Value;

            var number = ContactPhone.Create(request.Number).Value;

            var existingNumberVolunteer = await _volunteersRepository.GetByNumber(number);
            if (existingNumberVolunteer.IsSuccess)
                return Errors.General.AlreadyExist();

            var helpRequisituies = HelpRequisites.Create(request.HelpRequisities.PaymentMethod, request.HelpRequisities.Details).Value;

            var socialMediaResult = request.SocialMedias
                .Select(s => SocialMedia.Create(s.Name, s.Link).Value).ToList();

            var volunteerId = VolunteerId.NewVolunteerId();

            var volunteerResult = Volunteer.Create(
                volunteerId,
                fullName,
                request.Email,
                request.Description,
                request.YearsOfExperience,
                number,
                helpRequisituies,
                socialMediaResult);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            await _volunteersRepository.Add(volunteerResult.Value, cancellationToken);

            _logger.LogInformation(
                "Created volunteer {fullname} with id {volunteerId}", fullName, volunteerId);

            return (Guid)volunteerResult.Value.Id;
        }
    }
}
