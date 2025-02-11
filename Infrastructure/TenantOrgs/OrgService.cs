using Application.Features.TenantOrgs;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.TenantOrgs
{
    public class OrgService(ApplicationDbContext context) : IOrgService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<int> CreateOrgAsync(TenantOrg tenant)
        {
            await _context.TenantOrgs.AddAsync(tenant);
            await _context.SaveChangesAsync();
            return tenant.Id;
        }

        public async Task<int> DeleteOrgAsync(TenantOrg TenantOrg)
        {
            _context.TenantOrgs.Remove(TenantOrg);
            await _context.SaveChangesAsync();
            return TenantOrg.Id;
        }

        public Task<List<TenantOrg>> GetAllOrgsAsync()
        {
            return _context.TenantOrgs.ToListAsync();
        }

        public async Task<TenantOrg> GetOrgByIdAsync(int TenantOrgId)
        {
            var orgInDb = await _context
                .TenantOrgs.Where(o => o.Id == TenantOrgId)
                .FirstOrDefaultAsync();
            return orgInDb;
        }

        public async Task<TenantOrg> GetOrgByNameAsync(string name)
        {
            var orgInDb = await _context
                .TenantOrgs.Where(o => o.Name == name)
                .FirstOrDefaultAsync();
            return orgInDb;
        }

        public async Task<int> UpdateOrgAsync(TenantOrg tenant)
        {
            _context.TenantOrgs.Update(tenant);
            await _context.SaveChangesAsync();
            return tenant.Id;
        }
    }
}
