using Infrastructure.Tenancy;

namespace Infrastructure.OpenApi
{
    public class TenantHeaderAttribute()
        : SwaggerHeaderAttribute(
            TenancyConstants.TenantIdName,
                  "Enter your tenant name to access this API.",
                  string.Empty,
                  true)
    {
    }
}
