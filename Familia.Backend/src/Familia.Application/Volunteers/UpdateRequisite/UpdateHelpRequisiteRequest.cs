using Familia.Application.DTOs;
using Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects;

namespace Familia.Application.Volunteers.UpdateRequisite
{
    public record UpdateHelpRequisiteRequest(
        Guid VolunteerId,
        UpdateHelpRequisiteDto Dto);

    public record UpdateHelpRequisiteDto(HelpRequisitesDto HelpRequisites);
}
