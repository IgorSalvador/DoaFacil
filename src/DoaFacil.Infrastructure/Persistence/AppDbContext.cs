using DoaFacil.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoaFacil.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) 
    : IdentityDbContext<ApplicationUser, IdentityRole<long>, long, IdentityUserClaim<long>, IdentityUserRole<long>,
        IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>(options)
{
    public DbSet<DonationItem> DonationItems => Set<DonationItem>();
    public DbSet<DonationRequest> DonationRequests => Set<DonationRequest>();
    public DbSet<DonationStatusHistory> DonationStatusHistory => Set<DonationStatusHistory>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(builder);
    }
}
