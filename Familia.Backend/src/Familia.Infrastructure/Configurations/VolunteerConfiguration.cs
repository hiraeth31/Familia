using Familia.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Familia.Domain.Shared.Extenstions;
using Familia.Domain.Shared.EntityIds;
using Familia.Domain.Aggregates.VolunteerAggregate.AggregateRoot;

namespace Familia.Infrastructure.Configurations
{
    public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.ToTable("volunteers");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value))
                .HasColumnName("id");

            builder.ComplexProperty(v => v.FullName, b =>
            {
                b.Property(f => f.FirstName)
                .IsRequired()
                .HasMaxLength(Constants.LOW_TEXT_LENGTH)
                .HasColumnName("first_name");

                b.Property(f => f.LastName)
                .IsRequired()
                .HasMaxLength(Constants.LOW_TEXT_LENGTH)
                .HasColumnName("lastname_name");

                b.Property(f => f.Patronymic)
                .IsRequired()
                .HasMaxLength(Constants.LOW_TEXT_LENGTH)
                .HasColumnName("patronymic");
            });

            builder.Property(v => v.Email)
                .IsRequired()
                .HasMaxLength(Constants.LOW_TEXT_LENGTH)
                .HasColumnName("email");

            builder.Property(v => v.Description)
                .IsRequired()
                .HasMaxLength(Constants.MEDIUM_TEXT_LENGTH)
                .HasColumnName("description");

            builder.Property(v => v.YearsOfExperience)
                .IsRequired()
                .HasMaxLength(Constants.MEDIUM_TEXT_LENGTH)
                .HasColumnName("years_of_experience");

            builder.ComplexProperty(cp => cp.ContactPhone, b =>
            {
                b.Property(cp => cp.Phone)
                .IsRequired()
                .HasMaxLength(Constants.LOW_TEXT_LENGTH)
                .HasColumnName("contact_phone");
            });

            builder.ComplexProperty(hr => hr.HelpRequisites, b =>
            {
                b.Property(hr => hr.PaymentMethod)
                .IsRequired()
                .HasMaxLength(Constants.LOW_TEXT_LENGTH)
                .HasColumnName("payment_method");

                b.Property(hr => hr.Details)
                .IsRequired()
                .HasMaxLength(Constants.LOW_TEXT_LENGTH)
                .HasColumnName("details");
            });

            builder.Property(v => v.SocialMedias)
                .JsonValueObjectCollectionConversion()
                .HasColumnName("social_medias");

            builder.HasMany(v => v.Pets)
                .WithOne(p => p.Volunteer)
                .HasForeignKey("volunteer_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property<bool>("_isDeleted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("is_deleted")
                .HasDefaultValue(false); ;
        }
    }
}
