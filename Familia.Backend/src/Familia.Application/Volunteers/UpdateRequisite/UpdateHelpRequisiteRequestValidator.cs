using Familia.Application.Validation;
using Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects;
using Familia.Domain.Shared;
using FluentValidation;

namespace Familia.Application.Volunteers.UpdateRequisite
{
    public class UpdateHelpRequisiteRequestValidator : AbstractValidator<UpdateHelpRequisiteRequest>
    {
        public UpdateHelpRequisiteRequestValidator()
        {
            RuleFor(h => h.VolunteerId).NotEmpty()
                .WithError(Errors.General.ValueIsRequired());
        }
    }
    public class UpdateHelpRequisiteDtoValidator : AbstractValidator<UpdateHelpRequisiteDto>
    {
        public UpdateHelpRequisiteDtoValidator()
        {
            RuleFor(d => d.HelpRequisites)
                .MustBeValueObject(x => HelpRequisites.Create(x.PaymentMethod, x.Details));
        }
    }
}
