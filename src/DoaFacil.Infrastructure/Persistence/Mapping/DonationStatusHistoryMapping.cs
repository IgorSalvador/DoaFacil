using DoaFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoaFacil.Infrastructure.Persistence.Mapping;

public class DonationStatusHistoryMapping : IEntityTypeConfiguration<DonationStatusHistory>
{
    public void Configure(EntityTypeBuilder<DonationStatusHistory> builder)
    {
        builder.ToTable("donation_status_history");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DonationRequestId)
            .IsRequired();

        builder.Property(x => x.FromStatus);

        builder.Property(x => x.ToStatus)
            .IsRequired();

        builder.Property(x => x.ChangedByUserId)
            .IsRequired()
            .HasMaxLength(450);

        builder.Property(x => x.ChangedAtUtc)
            .IsRequired();

        builder.Property(x => x.Reason)
            .HasMaxLength(255);

        builder.HasIndex(x => new { x.DonationRequestId, x.ChangedAtUtc });
    }
}
