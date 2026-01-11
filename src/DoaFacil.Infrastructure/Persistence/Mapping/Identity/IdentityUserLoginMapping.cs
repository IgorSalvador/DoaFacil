using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoaFacil.Infrastructure.Persistence.Mapping.Identity;

public class IdentityUserLoginMapping : IEntityTypeConfiguration<IdentityUserLogin<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<Guid>> builder)
    {
        builder.ToTable("IdentityUserLogin");

        builder.HasKey(x => new
        {
            x.LoginProvider,
            x.ProviderKey
        });

        builder.Property(x => x.LoginProvider)
            .HasMaxLength(128);

        builder.Property(x => x.ProviderKey)
            .HasMaxLength(128);

        builder.Property(x => x.ProviderDisplayName)
            .HasMaxLength(255);
    }
}