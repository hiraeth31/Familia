using Familia.Domain.Aggregates.SpeciesAggregate.AggregateRoot;
using Familia.Domain.Shared;
using Familia.Domain.Shared.EntityIds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Familia.Infrastructure.Configurations
{
    public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
    {
        public void Configure(EntityTypeBuilder<Species> builder)
        {
            builder.ToTable("species");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value))
                .HasColumnName("id");

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(Constants.LOW_TEXT_LENGTH)
                .HasColumnName("name");

            builder.HasMany(s => s.Breeds)
                .WithOne(b => b.Species)
                .HasForeignKey("species_id")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
