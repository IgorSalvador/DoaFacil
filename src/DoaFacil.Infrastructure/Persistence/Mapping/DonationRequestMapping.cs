using DoaFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoaFacil.Infrastructure.Persistence.Mapping;

public class DonationRequestMapping : IEntityTypeConfiguration<DonationRequest>
{
    public void Configure(EntityTypeBuilder<DonationRequest> builder)
    {
        builder.ToTable("donation_requests");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ItemId)
            .IsRequired();

        builder.Property(x => x.RequesterUserId)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(x => x.RequestedQuantity)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.HasIndex(x => new { x.ItemId, x.Status });

        builder.HasMany(x => x.StatusHistory)
            .WithOne()
            .HasForeignKey(h => h.DonationRequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.StatusHistory)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
