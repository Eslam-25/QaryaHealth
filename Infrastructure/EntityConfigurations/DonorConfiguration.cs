using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class DonorConfiguration : IEntityTypeConfiguration<Donor>
{
    public void Configure(EntityTypeBuilder<Donor> builder)
    {
        builder.ToTable("donor");

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .UseIdentityColumn();

        builder.Property(x => x.IsActive)
            .HasColumnName("isActive");

        builder.Property(x => x.BirthDate)
            .HasColumnName("birth-date");

        builder.Property(x => x.LastDonationDate)
            .HasColumnName("last-donation-date");

        builder.Property(x => x.BloodType)
            .HasColumnName("blood-type");

        builder.Property(x => x.Gender)
            .HasColumnName("gender");

        builder.Property(x => x.ReadyToDonor)
            .HasColumnName("ready-to-donor");

        builder.Property(x => x.UserId)
            .HasColumnName("user-id");

        builder.HasOne(r => r.User)
            .WithOne(r => r.Donor)
            .HasForeignKey<Donor>(r => r.UserId);

    }
}
