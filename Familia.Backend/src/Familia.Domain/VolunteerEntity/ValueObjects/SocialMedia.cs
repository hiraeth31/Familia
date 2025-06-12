using CSharpFunctionalExtensions;

namespace Familia.Domain.VolunteerEntity.ValueObjects
{
    public record SocialMedia
    {
        private SocialMedia(string name, string link)
        {
            Name = name;
            Link = link;
        }
        public string Name { get; }
        public string Link { get; }

        public static Result<SocialMedia> Create(string name, string link)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<SocialMedia>("Название обязательно к заполнению!");

            if (string.IsNullOrWhiteSpace(link))
                return Result.Failure<SocialMedia>("Ссылка обязательна к заполнению!");

            return Result.Success(new SocialMedia(name, link));
        }
    }
}
