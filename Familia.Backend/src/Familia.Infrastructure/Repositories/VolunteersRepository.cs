using CSharpFunctionalExtensions;
using Familia.Application.Volunteers;
using Familia.Domain.Aggregates.VolunteerAggregate.AggregateRoot;
using Familia.Domain.Aggregates.VolunteerAggregate.ValueObjects;
using Familia.Domain.Shared;
using Familia.Domain.Shared.EntityIds;
using Microsoft.EntityFrameworkCore;

namespace Familia.Infrastructure.Repositories
{
    public class VolunteersRepository : IVolunteersRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public VolunteersRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Add(
            Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return volunteer.Id;
        }

        public async Task<Guid> Save(
            Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            _dbContext.Volunteers.Attach(volunteer);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return volunteer.Id;
        }

        public async Task<Result<Volunteer, Error>> GetById(
            VolunteerId volunteerId, CancellationToken cancellationToken = default)
        {
            var volunteer = await _dbContext.Volunteers
                .Include(v => v.Pets)
                .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);

            if (volunteer is null)
                return Errors.General.NotFound(volunteerId);

            return volunteer;
        }

        public async Task<Result<Volunteer, Error>> GetByNumber(
            ContactPhone number, CancellationToken cancellationToken = default)
        {
            var volunteer = await _dbContext.Volunteers
                .Include(v => v.Pets)
                .FirstOrDefaultAsync(n => n.ContactPhone == number, cancellationToken);

            if (volunteer is null)
                return Errors.General.NotFound();

            return volunteer;
        }

        public async Task<Guid> Delete(
            Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            _dbContext.Volunteers.Remove(volunteer);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return volunteer.Id;
        }
    }
}
