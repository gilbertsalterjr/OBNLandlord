using Finbuckle.MultiTenant;
using Infrastructure.Tenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistance.DbInitializers
{
    internal class TenantDbInitializer(TenantDbContext tenantDbContext,IServiceProvider serviceProvider) : ITenantDbInitializer
    {
        private readonly TenantDbContext _tenantDbContext = tenantDbContext;
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task InitializeDatabaseAsync(CancellationToken cancellationToken)
        {
            await InitializeDatabaseWithTenantAsync(cancellationToken);

            foreach (var tenant in await _tenantDbContext.TenantInfo.ToListAsync(cancellationToken))
            {
                await InitializeApplicationDbForTenantAsync(tenant, cancellationToken);
            }
            
        }

        private async Task InitializeDatabaseWithTenantAsync(CancellationToken cancellationToken)
        {
            if (await _tenantDbContext.TenantInfo.FindAsync([TenancyConstants.Root.Id], cancellationToken)
                is null)
            {
                var rootTenant = new OBNTenantInfo
                {
                    Id = TenancyConstants.Root.Id,
                    Identifier = TenancyConstants.Root.Name,
                    Name = TenancyConstants.Root.Name,
                    AdminEmail = TenancyConstants.Root.Email,
                    IsActive = true,
                    ValidUpTo = DateTime.UtcNow.AddYears(1)
                };

                await _tenantDbContext.TenantInfo.AddAsync(rootTenant, cancellationToken);
                await _tenantDbContext.SaveChangesAsync(cancellationToken);
            }               
        }

        private async Task InitializeApplicationDbForTenantAsync(OBNTenantInfo tenant, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            _serviceProvider.GetRequiredService<IMultiTenantContextAccessor>()
                .MultiTenantContext = new MultiTenantContext<OBNTenantInfo>()
                {
                    TenantInfo = tenant
                };

            await _serviceProvider.GetRequiredService<ApplicationDbInitializer>()
                .InitializeDatabaseAsync(cancellationToken);
        }

        
    }
}
