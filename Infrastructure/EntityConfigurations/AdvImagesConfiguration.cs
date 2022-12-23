using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class AdvImagesConfiguration : IEntityTypeConfiguration<AdvImage>
{
    public void Configure(EntityTypeBuilder<AdvImage> builder)
    {
        builder.ToTable("adv-images");

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .UseIdentityColumn();

        builder.Property(x => x.IsActive)
            .HasColumnName("isActive");

        builder.Property(x => x.ImagePath)
            .HasColumnName("image-path");

        builder.Property(x => x.UserId)
            .HasColumnName("user-id");

        builder.HasOne(x => x.User)
            .WithMany(r => r.AdvImages)
            .HasForeignKey(r => r.UserId);
    }
}
