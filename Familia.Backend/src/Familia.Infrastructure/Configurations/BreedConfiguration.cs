
using Familia.Domain.BreedEntity;
using Familia.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Familia.Infrastructure.Configurations
{
    public class BreedConfiguration : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            builder.ToTable("breeds");

            builder.HasKey(b=> b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                id => id.Value,
                value => BreedId.Create(value))
                .HasColumnName("id");

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(Constants.LOW_TEXT_LENGTH)
                .HasColumnName("name");
        }
    }
}
