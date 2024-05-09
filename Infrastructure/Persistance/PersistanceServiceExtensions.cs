﻿using Infrastructure.Persistance.Contexts;
using Infrastructure.Persistance.DbInitializers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance
{
    public static class PersistanceServiceExtensions
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDbContext<ApplicationDbContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                .AddTransient<ITenantDbInitializer, TenantDbInitializer>()
                .AddTransient<ApplicationDbInitializer>();
        }

        public static async Task AddDatabaseInitializerAsync(this IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
        {
            using var scope = serviceProvider.CreateScope();
            
            await scope.ServiceProvider.GetRequiredService<ITenantDbInitializer>()
                .InitializeDatabaseAsync(cancellationToken);
        }
    }
}
