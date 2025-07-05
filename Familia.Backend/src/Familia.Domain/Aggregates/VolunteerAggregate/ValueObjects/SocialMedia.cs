using CSharpFunctionalExtensions;
using Familia.Domain.Shared;
using System.Text.Json.Serialization;

namespace Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects
{
    public record SocialMedia
    {
        [JsonConstructor]
        private SocialMedia(string name, string link)
        {
            Name = name;
            Link = link;
        }
        public string Name { get; }
        public string Link { get; }

        public static Result<SocialMedia, Error> Create(string name, string link)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Errors.General.ValueIsInvalid("Название");

            if (string.IsNullOrWhiteSpace(link))
                return Errors.General.ValueIsInvalid("Ссылка");

            return new SocialMedia(name, link);
        }
    }
}
