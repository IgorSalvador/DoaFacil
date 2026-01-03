using DoaFacil.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoaFacil.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : IdentityDbContext<ApplicationUser, IdentityRole<long>, long, IdentityUserClaim<long>, IdentityUserRole<long>,
        IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>(options)
{
    public DbSet<DonationItem> DonationItems { get; set; }
    public DbSet<DonationRequest> DonationRequests { get; set; }
    public DbSet<DonationStatusHistory> DonationStatusHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
