using DoaFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoaFacil.Infrastructure.Persistence.Mapping;

public class DonationItemMapping : IEntityTypeConfiguration<DonationItem>
{
    public void Configure(EntityTypeBuilder<DonationItem> builder)
    {
        builder.ToTable("donation_items");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.OwnerUserId)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Category)
            .IsRequired();

        builder.Property(x => x.Condition)
            .IsRequired();

        builder.Property(x => x.QuantityAvailable)
            .IsRequired();

        builder.Property(x => x.ApproxLocation)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.HasIndex(x => x.OwnerUserId);
        builder.HasIndex(x => new { x.IsActive, x.QuantityAvailable });
    }
}
