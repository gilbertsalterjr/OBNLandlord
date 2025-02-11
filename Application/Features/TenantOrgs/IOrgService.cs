using Domain.Entities;

namespace Application.Features.TenantOrgs
{
    public interface IOrgService
    {
        // CRUD

        Task<int> CreateOrgAsync(TenantOrg tenant);
        Task<int> UpdateOrgAsync(TenantOrg tenant);
        Task<int> DeleteOrgAsync(TenantOrg TenantOrg);

        Task<TenantOrg> GetOrgByIdAsync(int TenantOrgId);
        Task<List<TenantOrg>> GetAllOrgsAsync();
        Task<TenantOrg> GetOrgByNameAsync(string name);


    }
}
