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

        public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return volunteer.Id;
        }

        public async Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId)
        {
            var volunteer = await _dbContext.Volunteers
                .Include(v => v.Pets)
                .FirstOrDefaultAsync(v => v.Id == volunteerId);

            if (volunteer is null)
                return Errors.General.NotFound(volunteerId);

            return volunteer;
        }

        public async Task<Result<Volunteer, Error>> GetByNumber(ContactPhone number)
        {
            var volunteer = await _dbContext.Volunteers
                .Include(v => v.Pets)
                .FirstOrDefaultAsync(n => n.ContactPhone == number);

            if (volunteer is null)
                return Errors.General.NotFound();

            return volunteer;
        }
    }
}
