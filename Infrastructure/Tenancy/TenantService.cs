using Application.Features.Tenacy;
using Application.Features.Tenacy.Models;
using Finbuckle.MultiTenant;
using Infrastructure.Persistence.DbInitializers;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Tenancy
{
    public class TenantService(IMultiTenantStore<OBNTenantInfo> tenantStore,
        ApplicationDbInitializer applicationDbInitializer,
        IServiceProvider serviceProvider) : ITenantService
    {
        private readonly IMultiTenantStore<OBNTenantInfo> _tenantStore = tenantStore;
        private readonly ApplicationDbInitializer _applicationDbInitializer = applicationDbInitializer;
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task<string> ActivateAsync(string id)
        {
            var tenantInDb = await _tenantStore.TryGetAsync(id);
            tenantInDb.IsActive = true;

            await _tenantStore.TryUpdateAsync(tenantInDb);
            return tenantInDb.Id;
        }


        public async Task<string> CreateTenantAsync(CreateTenantRequest createTenant, CancellationToken ct)
        {
            //Create tenant in db
            var newTenant = new OBNTenantInfo 
            { 
                Id = createTenant.Identifier,
                Identifier = createTenant.Identifier,
                Name = createTenant.Name, 
                ConnectionString = createTenant.ConnectionString,
                AdminEmail = createTenant.AdminEmail,
                ValidUpTo = createTenant.ValidUpTo,
                IsActive = createTenant.IsActive
            };

            await _tenantStore.TryAddAsync(newTenant);

            // Initialize tenant with Users, User Roles, Roles and Role Permissions

            try
            {
                using var scope = _serviceProvider.CreateScope();
                _serviceProvider.GetRequiredService<IMultiTenantContextAccessor>()
                    .MultiTenantContext = new MultiTenantContext<OBNTenantInfo>()
                    {
                        TenantInfo = newTenant
                    };
                await scope.ServiceProvider.GetRequiredService<ApplicationDbInitializer>()
                    .InitializeDatabaseAsync(ct);
            }
            catch (Exception)
            {
                await _tenantStore.TryRemoveAsync(createTenant.Identifier);
                throw;
            }            

            return newTenant.Id;
        }

        public async Task<string> DeactivateAsync(string id)
        {
            var tenantInDb = await _tenantStore.TryGetAsync(id);
            tenantInDb.IsActive = false;

            await _tenantStore.TryUpdateAsync(tenantInDb);
            return tenantInDb.Id;
        }

        public async Task<TenantDto> GetTenantByIdAsync(string id)
        {
            var tenantInDb = await _tenantStore.TryGetAsync(id);

            #region Manual Mapping - Opt 1
            //return new TenantDto()
            //{
            //    Id = tenantInDb.Id,
            //    Name = tenantInDb.Name,
            //    AdminEmail = tenantInDb.AdminEmail,
            //    ConnectionString = tenantInDb.ConnectionString,
            //    ValidUpTo = tenantInDb.ValidUpTo,
            //    IsActive = tenantInDb.IsActive
            //};
            #endregion

            return tenantInDb.Adapt<TenantDto>();
        }

        public async Task<List<TenantDto>> GetTenantsAsync()
        {
           var tenantsInDb = await _tenantStore.GetAllAsync();
            return tenantsInDb.Adapt<List<TenantDto>>();
        }

        public async Task<string> UpdateSubscriptionAsync(string id, DateTime newExpiryDate)
        {
            var tenantInDb = await _tenantStore.TryGetAsync(id);
            tenantInDb.ValidUpTo = newExpiryDate;
            return tenantInDb.Id;
        }
    }
}
