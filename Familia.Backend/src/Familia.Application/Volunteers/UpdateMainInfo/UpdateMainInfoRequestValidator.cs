using Familia.Application.Validation;
using Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects;
using Familia.Domain.Shared;
using FluentValidation;

namespace Familia.Application.Volunteers.UpdateMainInfo
{
    public class UpdateMainInfoRequestValidator : AbstractValidator<UpdateMainInfoRequest>
    {
        public UpdateMainInfoRequestValidator()
        {
            RuleFor(v => v.VolunteerId).NotEmpty()
                .WithError(Errors.General.ValueIsRequired());
        }
    }
    public class UpdateMainInfoDtoValidator : AbstractValidator<UpdateMainInfoDto>
    {
        public UpdateMainInfoDtoValidator()
        {
            RuleFor(c => c.FullName)
                .MustBeValueObject(fn => FullName.Create(
                    fn.FirstName, fn.LastName, fn.Patronymic));

            RuleFor(c => c.Number)
                .MustBeValueObject(ContactPhone.Create);

            RuleFor(c => c.YearsOfExperience).GreaterThanOrEqualTo(0)
                .WithError(Errors.General.ValueIsInvalid("Years of experience"));
        }
    }
}
