using CSharpFunctionalExtensions;
using Familia.Domain.Aggregates.VolunteerAggregate.AggregateRoot;
using Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects;
using Familia.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace Familia.Application.Volunteers.UpdateRequisite
{
    public class UpdateHelpRequisiteHandler
    {
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly ILogger<UpdateHelpRequisiteHandler> _logger;

        public UpdateHelpRequisiteHandler(
            IVolunteersRepository volunteersRepository,
            ILogger<UpdateHelpRequisiteHandler> logger)
        {
            _volunteersRepository = volunteersRepository;
            _logger = logger;
        }

        public async Task<Result<Guid, Error>> Handle(
            UpdateHelpRequisiteRequest request,
            CancellationToken cancellationToken = default)
        {
            var volunteerResult = await _volunteersRepository.GetById(request.VolunteerId, cancellationToken);
            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            var helpRequisite = HelpRequisites.Create(
                request.Dto.HelpRequisites.PaymentMethod,
                request.Dto.HelpRequisites.Details).Value;

            volunteerResult.Value.UpdateHelpRequisite(helpRequisite);

            var result = await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("Help requisite updated for volunteer with Id {volunteerId}", request.VolunteerId);

            return result;
        }
    }
}
