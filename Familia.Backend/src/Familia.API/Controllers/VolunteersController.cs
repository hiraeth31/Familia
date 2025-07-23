using Familia.API.Extensions;
using Familia.Application.Volunteers.CreateVolunteer;
using Familia.Application.Volunteers.Delete;
using Familia.Application.Volunteers.UpdateMainInfo;
using Familia.Application.Volunteers.UpdateRequisite;
using Familia.Application.Volunteers.UpdateSocialMedia;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace Familia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VolunteersController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Create(
            [FromServices] CreateVolunteerHandler handler,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}/main-info")]
        public async Task<ActionResult> UpdateMainInfo(
            [FromRoute] Guid id,
            [FromServices] UpdateMainInfoHandler handler,
            [FromBody] UpdateMainInfoDto dto,
            [FromServices] IValidator<UpdateMainInfoRequest> validator,
            CancellationToken cancellationToken)
        {
            var request = new UpdateMainInfoRequest(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }

            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}/social-media")]
        public async Task<ActionResult> UpdateSocialMedia(
            [FromRoute] Guid id,
            [FromServices] UpdateSocialMediaHandler handler,
            [FromBody] UpdateSocialMediaDto dto,
            [FromServices] IValidator<UpdateSocialMediaRequest> validator,
            CancellationToken cancellationToken)
        {
            var request = new UpdateSocialMediaRequest(id, dto);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }

            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}/help-requisite")]
        public async Task<ActionResult> UpdateHelpRequisite(
            [FromRoute] Guid id,
            [FromServices] UpdateHelpRequisiteHandler handler,
            [FromBody] UpdateHelpRequisiteDto dto,
            [FromServices] IValidator<UpdateHelpRequisiteRequest> validator)
        {
            var request = new UpdateHelpRequisiteRequest(id, dto);

            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }

            var result = await handler.Handle(request);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}/hard")]
        public async Task<ActionResult> DeleteHard(
            [FromRoute] Guid id,
            [FromServices] DeleteHardVolunteerHandler handler,
            [FromServices] IValidator<DeleteVolunteerRequest> validator,
            CancellationToken cancellationToken)
        {
            var request = new DeleteVolunteerRequest(id);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }

            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}/soft")]
        public async Task<ActionResult> DeleteSoft(
            [FromRoute] Guid id,
            [FromServices] DeleteSoftVolunteerHandler handler,
            [FromServices] IValidator<DeleteVolunteerRequest> validator,
            CancellationToken cancellationToken)
        {
            var request = new DeleteVolunteerRequest(id);

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }

            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }
    }
}
