using Familia.Domain.Aggregates.VolunteerAggregate.Entities;
using Familia.Domain.Shared;
using Familia.Domain.Shared.EntityIds;
using Familia.Domain.Shared.Extenstions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Familia.Infrastructure.Configurations
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("pets");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                id => id.Value,
                value => PetId.Create(value))
                .HasColumnName("id");

            builder.OwnsOne(p => p.SpeciesBreed, sb =>
            {
                sb.Property(p => p.SpeciesId)
                .HasConversion(
                    id => id.Value,
                    value => SpeciesId.Create(value))
                .HasColumnName("species_id");

                sb.Property(p => p.BreedId)
                .HasConversion(
                    id => id.Value,
                    value => BreedId.Create(value))
                .HasColumnName("breed_id");
            });

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(Constants.LOW_TEXT_LENGTH)
                .HasColumnName("name");

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(Constants.LOW_TEXT_LENGTH)
                .HasColumnName("description");

            builder.Property(p => p.Color)
                .IsRequired()
                .HasMaxLength(Constants.LOW_TEXT_LENGTH)
                .HasColumnName("color");

            builder.Property(p => p.HealthInfo)
                .IsRequired()
                .HasMaxLength(Constants.LOW_TEXT_LENGTH)
                .HasColumnName("health_info");

            builder.ComplexProperty(p => p.Position, pb =>
            {
                pb.Property(p => p.Value)
                .IsRequired()
                .HasColumnName("position");
            });

            builder.ComplexProperty(p => p.Address, pb =>
            {
                pb.Property(ad => ad.Country)
                .IsRequired()
                .HasColumnName("country")
                .HasMaxLength(Constants.LOW_TEXT_LENGTH);

                pb.Property(ad => ad.City)
                .IsRequired()
                .HasColumnName("city")
                .HasMaxLength(Constants.LOW_TEXT_LENGTH);

                pb.Property(ad => ad.Street)
                .IsRequired()
                .HasColumnName("street")
                .HasMaxLength(Constants.LOW_TEXT_LENGTH);

                pb.Property(ad => ad.House)
               .IsRequired()
               .HasColumnName("house")
               .HasMaxLength(Constants.LOW_TEXT_LENGTH);
            });

            builder.ComplexProperty(p => p.BodyMeasurements, bb =>
            {
                bb.Property(b => b.Height)
                .IsRequired()
                .HasColumnName("height");

                bb.Property(b => b.Weight)
                .IsRequired()
                .HasColumnName("weight");
            });

            builder.ComplexProperty(p => p.PhoneNumber, bb =>
            {
                bb.Property(p => p.Phone)
                .IsRequired()
                .HasMaxLength(Constants.LOW_TEXT_LENGTH)
                .HasColumnName("phone");
            });

            builder.ComplexProperty(p => p.HelpRequisites, hb =>
            {
                hb.Property(h => h.PaymentMethod)
                .IsRequired()
                .HasMaxLength(Constants.LOW_TEXT_LENGTH)
                .HasColumnName("payment_method");

                hb.Property(h => h.Details)
                .IsRequired()
                .HasMaxLength(Constants.MEDIUM_TEXT_LENGTH)
                .HasColumnName("payment_method");
            });

            builder.Property(p => p.IsNeutered)
                .IsRequired()
                .HasColumnName("is_neutered");

            builder.Property(p => p.Birthday)
                .SetDefaultDateTimeKind(DateTimeKind.Utc)
                .HasColumnName("birthday")
                .IsRequired();

            builder.Property(p => p.IsVaccinated)
                .IsRequired()
                .HasColumnName("is_vaccinated");

            builder.ComplexProperty(p => p.HelpStatus, hb =>
            {
                hb.Property(h => h.Status)
                .IsRequired()
                .HasColumnName("help_status");
            });

            builder.Property(p => p.CreationDate)
                .SetDefaultDateTimeKind(DateTimeKind.Utc)
                .HasColumnName("creation_date")
                .IsRequired();

            builder.Property<bool>("_isDeleted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("is_deleted");
        }
    }
}
