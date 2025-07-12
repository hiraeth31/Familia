using Familia.Application.Validation;
using Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects;
using FluentValidation;

namespace Familia.Application.Volunteers.CreateVolunteer
{
    public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
    {
        public CreateVolunteerRequestValidator()
        {
            RuleFor(c => c.FullName)
                .MustBeValueObject(fn => FullName.Create(
                    fn.FirstName, fn.LastName, fn.Patronymic));

            RuleFor(c => c.Number)
                .MustBeValueObject(ContactPhone.Create);

            RuleForEach(c => c.SocialMedias).
                MustBeValueObject(s => SocialMedia.Create(s.Name, s.Link));

            RuleFor(c => c.HelpRequisities)
                .MustBeValueObject(hr => HelpRequisites.Create(
                    hr.PaymentMethod, hr.Details));
        }
    }
}
