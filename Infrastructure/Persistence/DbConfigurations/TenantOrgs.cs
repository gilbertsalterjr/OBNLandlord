using Domain.Entities;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.DbConfigurations
{
    internal class TenantOrgConfig : IEntityTypeConfiguration<TenantOrg>
    {
        public void Configure(EntityTypeBuilder<TenantOrg> builder)
        {
            builder
                .ToTable("TenantOrgs", SchemaNames.TenantOrgs)
                .IsMultiTenant();

            builder
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(60);
        }
    }
}
