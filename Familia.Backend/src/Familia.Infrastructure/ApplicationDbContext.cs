using Familia.Domain.Aggregates.SpeciesAggregate.AggregateRoot;
using Familia.Domain.Aggregates.VolunteerAggregate.AggregateRoot;
using Familia.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Familia.Infrastructure
{
    public class ApplicationDbContext(IConfiguration configuration) : DbContext
    {
        private const string DATABASE = "Database";
        public DbSet<Volunteer> Volunteers => Set<Volunteer>();
        public DbSet<Species> Species => Set<Species>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(DATABASE));
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
            optionsBuilder.EnableSensitiveDataLogging();

            //optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        private ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}
