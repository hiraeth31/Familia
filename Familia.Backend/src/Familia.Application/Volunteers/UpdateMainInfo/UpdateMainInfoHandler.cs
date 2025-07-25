using CSharpFunctionalExtensions;
using Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects;
using Familia.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace Familia.Application.Volunteers.UpdateMainInfo
{
    public class UpdateMainInfoHandler
    {
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly ILogger<UpdateMainInfoHandler> _logger;

        public UpdateMainInfoHandler(
            IVolunteersRepository volunteersRepository,
            ILogger<UpdateMainInfoHandler> logger)
        {
            _volunteersRepository = volunteersRepository;
            _logger = logger;
        }

        public async Task<Result<Guid, Error>> Handle(
            UpdateMainInfoRequest request,
            CancellationToken cancellationToken = default)
        {
            var volunteerResult = await _volunteersRepository.GetById(request.VolunteerId, cancellationToken);
            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            var fullName = FullName.Create(
                request.Dto.FullName.FirstName,
                request.Dto.FullName.LastName,
                request.Dto.FullName.Patronymic).Value;

            var number = ContactPhone.Create(request.Dto.Number).Value;

            volunteerResult.Value.UpdateMainInfo(
                fullName,
                request.Dto.Email,
                request.Dto.Description,
                request.Dto.YearsOfExperience,
                number);

            var result = await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);

            _logger.LogInformation(
                "Updated volunteer's main info with id {volunteerId}", request.VolunteerId);

            return result;
        }
    }
}
