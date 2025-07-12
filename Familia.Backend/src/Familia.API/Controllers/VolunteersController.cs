using Familia.API.Extensions;
using Familia.API.Response;
using Familia.Application.Volunteers.CreateVolunteer;
using Familia.Domain.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Familia.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VolunteersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

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
    }
}
