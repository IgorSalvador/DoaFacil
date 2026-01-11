using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoaFacil.Infrastructure.Persistence.Mapping.Identity;

public class IdentityRoleClaimMapping : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> builder)
    {
        builder.ToTable("IdentityRoleClaim");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ClaimType)
            .HasMaxLength(255);

        builder.Property(x => x.ClaimValue)
            .HasMaxLength(255);
    }
}
