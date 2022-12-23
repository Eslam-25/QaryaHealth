using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class MedicalStaffServiceConfiguration : IEntityTypeConfiguration<MedicalStaffServiceType>
{
    public void Configure(EntityTypeBuilder<MedicalStaffServiceType> builder)
    {
        builder.ToTable("medical-staff-service");

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .UseIdentityColumn();

        builder.Property(x => x.IsActive)
            .HasColumnName("isActive");

        builder.Property(x => x.Name)
            .HasColumnName("name");

        builder.Property(x => x.Description)
            .HasColumnName("description");

        builder.Property(x => x.Cost)
            .HasColumnName("cost");

        builder.Property(x => x.StaffId)
            .HasColumnName("staff-id");

        builder.HasOne(r => r.MedicalStaff)
            .WithMany(r => r.MedicalStaffServices)
            .HasForeignKey(r => r.StaffId);

    }
}
