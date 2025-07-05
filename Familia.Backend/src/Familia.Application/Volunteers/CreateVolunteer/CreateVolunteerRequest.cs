using Familia.Application.DTOs;

namespace Familia.Application.Volunteers.CreateVolunteer
{
    public record CreateVolunteerRequest(
        FullNameDto FullName,
        string Description,
        int YearsOfExperience,
        string Email,
        string Number,
        List<SocialMediaDto> SocialMedias,
        HelpRequisitiesDto HelpRequisities);
}
