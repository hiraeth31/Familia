using Familia.Application.Validation;
using Familia.Domain.Shared;
using FluentValidation;

namespace Familia.Application.Volunteers.Delete
{
    public class DeleteVolunteerRequestValidator: AbstractValidator<DeleteVolunteerRequest>
    {
        public DeleteVolunteerRequestValidator()
        {
            RuleFor(d => d.VolunteerId).NotEmpty()
                .WithError(Errors.General.ValueIsRequired()); ;
        }
    }
}
