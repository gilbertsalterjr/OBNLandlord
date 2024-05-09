using Infrastructure.Identity;
using Infrastructure.OpenApi;
using Infrastructure.Persistance;
using Infrastructure.Tenancy;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceCollectionExensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) 
        {
            return services
                .AddMultitenancyService (configuration)
                .AddPersistenceService(configuration)
                .AddIdentityServices()
                .AddPermissions()
                .AddJwtAuthentication()
                .AddOpenApiDocumentation(configuration);
        }

        public static IApplicationBuilder UserInfrastructure(this IApplicationBuilder app)
        {
            return app
                .UseCurrentUser()
                .UseOpenApiDocumentation();
        }
    }
}
