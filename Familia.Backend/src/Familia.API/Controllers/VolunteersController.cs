using Familia.API.Extensions;
using Familia.API.Response;
using Familia.Application.Volunteers.CreateVolunteer;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<Guid>> Create(
            [FromServices] CreateVolunteerHandler handler,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(request, cancellationToken);

            //if (result.IsFailure)
            //    return result.Error.ToResponse();

            return result.ToResponse();
        }
    }
}
