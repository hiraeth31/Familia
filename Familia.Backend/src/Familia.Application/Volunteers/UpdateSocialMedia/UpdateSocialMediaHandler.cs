using CSharpFunctionalExtensions;
using Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects;
using Familia.Domain.Shared;
using Microsoft.Extensions.Logging;

namespace Familia.Application.Volunteers.UpdateSocialMedia
{
    public class UpdateSocialMediaHandler
    {
        private readonly IVolunteersRepository _volunteersRepository;
        private readonly ILogger<UpdateSocialMediaHandler> _logger;

        public UpdateSocialMediaHandler(
            IVolunteersRepository volunteersRepository,
            ILogger<UpdateSocialMediaHandler> logger)
        {
            _volunteersRepository = volunteersRepository;
            _logger = logger;
        }

        public async Task<Result<Guid, Error>> Handle(
            UpdateSocialMediaRequest request,
            CancellationToken cancellationToken = default)
        {
            var volunteerResult = await _volunteersRepository.GetById(request.VolunteerId, cancellationToken);
            if (volunteerResult.IsFailure)
                return volunteerResult.Error;

            var socialMedia = request.Dto.SocialMedia.Select(
                s => SocialMedia.Create(s.Name, s.Link).Value);

            volunteerResult.Value.UpdateSocialMedia(socialMedia);

            var result = await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("Social media updated for volunteer with Id {volunteerId}", request.VolunteerId);

            return result;
        }
    }
}
