using Familia.Application.DTOs;

namespace Familia.Application.Volunteers.CreateVolunteer
{
    public record CreateVolunteerRequest(
        FullNameDto FullName,
        string Description,
        int YearsOfExperience,
        string Email,
        string Number,
        IEnumerable<SocialMediaDto> SocialMedias,
        HelpRequisitiesDto HelpRequisities);
}
