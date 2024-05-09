using Infrastructure.Tenancy;

namespace Infrastructure.Persistance.DbInitializers
{
    internal interface ITenantDbInitializer
    {
        Task InitializeDatabaseAsync(CancellationToken cancellationToken);
        //Task InitializeDatabaseWithTenantAsync(CancellationToken cancellationToken);
        //Task InitializeApplicationDbForTenantAsync(OBNTenantInfo tenant, CancellationToken cancellationToken);

    }
}
