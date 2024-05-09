using Finbuckle.MultiTenant.Stores;
using Infrastructure.Persistance.DbConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tenancy
{
    public class TenantDbContext(DbContextOptions<TenantDbContext> options)
        : EFCoreStoreDbContext<OBNTenantInfo>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OBNTenantInfo>()
                .ToTable("Tenants", SchemaNames.Multitenancy);
        }
    }
}
