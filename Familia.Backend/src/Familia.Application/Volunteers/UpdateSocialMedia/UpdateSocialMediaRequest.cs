using Familia.Application.DTOs;

namespace Familia.Application.Volunteers.UpdateSocialMedia
{
    public record UpdateSocialMediaRequest(
        Guid VolunteerId,
        UpdateSocialMediaDto Dto);

    public record UpdateSocialMediaDto(IEnumerable<SocialMediaDto> SocialMedia);
}
