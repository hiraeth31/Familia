using Familia.Application.DTOs;

namespace Familia.Application.Volunteers.CreateVolunteer
{
    public record CreateVolunteerRequest(
        FullNameDTO FullName,
        string Description,
        int YearsOfExperience,
        string Email,
        string Number,
        List<SocialMediaDTO> SocialMedias,
        HelpRequisitiesDTO HelpRequisities);
}
