using Familia.Application.Validation;
using Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects;
using Familia.Domain.Shared;
using FluentValidation;

namespace Familia.Application.Volunteers.UpdateSocialMedia
{
    public class UpdateSocialMediaRequestValidator : AbstractValidator<UpdateSocialMediaRequest>
    {
        public UpdateSocialMediaRequestValidator()
        {
            RuleFor(v => v.VolunteerId).NotEmpty()
                .WithError(Errors.General.ValueIsRequired());
        }
    }
    public class UpdateSocialMediaDtoValidator : AbstractValidator<UpdateSocialMediaDto>
    {
        public UpdateSocialMediaDtoValidator()
        {
            RuleForEach(s => s.SocialMedia)
                .MustBeValueObject(s => SocialMedia.Create(s.Name, s.Link));
        }
    }
}
