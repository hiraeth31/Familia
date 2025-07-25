using Familia.Application.DTOs;

namespace Familia.Application.Volunteers.UpdateMainInfo
{
    public record UpdateMainInfoRequest(
        Guid VolunteerId,
        UpdateMainInfoDto Dto);

    public record UpdateMainInfoDto(
        FullNameDto FullName,
        string Email,
        string Description,
        int YearsOfExperience,
        string Number);
}
