using Infrastructure.Tenancy;

namespace Infrastructure.Persistence.DbInitializers
{
    internal interface ITenantDbInitializer
    {
        Task InitializeDatabaseAsync(CancellationToken cancellationToken);        

    }
}
