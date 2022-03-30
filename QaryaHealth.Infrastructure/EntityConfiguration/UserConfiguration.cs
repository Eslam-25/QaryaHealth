using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QaryaHealth.Core.Entities;

namespace QaryaHealth.Infrastructure.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(x => x.IsActive)
                .HasColumnName("isActive");

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(x => x.Password)
                .HasColumnName("password")
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasColumnName("phone-number")
                .IsRequired();

            builder.HasIndex("PhoneNumber")
                .IsUnique();

            builder.Property(x => x.Role)
              .HasColumnName("role")
              .IsRequired();

            builder.Property(x => x.BloodType)
              .HasColumnName("blood-type")
              .IsRequired();
        }
    }
}
