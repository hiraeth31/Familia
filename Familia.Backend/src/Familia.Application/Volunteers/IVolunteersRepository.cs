using CSharpFunctionalExtensions;
using Familia.Domain.Aggregates.VolunteerAggregate.AggregateRoot;
using Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects;
using Familia.Domain.Shared;
using Familia.Domain.Shared.EntityIds;

namespace Familia.Application.Volunteers
{
    public interface IVolunteersRepository
    {
        Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
        Task<Guid> Save(Volunteer volunteer, CancellationToken cancellationToken = default);
        Task<Guid> Delete(Volunteer volunteer, CancellationToken cancellationToken = default);
        Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken = default);
        Task<Result<Volunteer, Error>> GetByNumber(ContactPhone number, CancellationToken cancellationToken = default);
    }
}
