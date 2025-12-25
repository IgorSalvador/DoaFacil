using DoaFacil.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoaFacil.Infrastructure.Persistence.Domain;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
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
