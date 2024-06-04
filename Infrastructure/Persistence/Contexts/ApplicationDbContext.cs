using Domain.Entities;
using Finbuckle.MultiTenant;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext(ITenantInfo tenantInfo, DbContextOptions<ApplicationDbContext> options)
        : BaseDbContext(tenantInfo, options)
    {
        public DbSet<TenantOrg> TenantOrgs => Set<TenantOrg>();
    }
}
