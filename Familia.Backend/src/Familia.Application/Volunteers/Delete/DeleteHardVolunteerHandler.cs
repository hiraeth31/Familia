using CSharpFunctionalExtensions;
using Familia.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace Familia.Application.Volunteers.Delete
{
    public class DeleteHardVolunteerHandler
    {
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly ILogger<DeleteHardVolunteerHandler> _logger;

        public DeleteHardVolunteerHandler(
            IVolunteersRepository volunteersRepository,
            ILogger<DeleteHardVolunteerHandler> logger)
        {
            _volunteersRepository = volunteersRepository;
            _logger = logger;
        }

        public async Task<Result<Guid, Error>> Handle(
            DeleteVolunteerRequest request,
            CancellationToken cancellationToken = default)
        {
            var volunteerResult = await _volunteersRepository.GetById(request.VolunteerId);
            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            var result = await _volunteersRepository.Delete(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("Volunteer hard deleted with id {volunteerId}", request.VolunteerId);

            return result;
        }
    }
}
