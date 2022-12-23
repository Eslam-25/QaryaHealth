using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class MedicalStaffConfiguration : IEntityTypeConfiguration<MedicalStaff>
{
    public void Configure(EntityTypeBuilder<MedicalStaff> builder)
    {
        builder.ToTable("medical-staff");

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .UseIdentityColumn();

        builder.Property(x => x.IsActive)
            .HasColumnName("isActive");

        builder.Property(x => x.Type)
            .HasColumnName("type");

        builder.Property(x => x.Bio)
            .HasColumnName("bio");

        builder.Property(x => x.WorkingDescription)
            .HasColumnName("working-description");

        builder.Property(x => x.ImagePath)
            .HasColumnName("image-path");

        builder.Property(x => x.ReadyToWork)
            .HasColumnName("ready-to-work");

        builder.Property(x => x.UserId)
            .HasColumnName("user-id");

        builder.HasOne(r => r.User)
            .WithOne(r => r.MedicalStaff)
            .HasForeignKey<MedicalStaff>(r => r.UserId);

    }
}
