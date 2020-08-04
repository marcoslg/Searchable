using Authorize.Application;
using Authorize.Application.Contracts;
using Authorize.Infrastructure.Persistence.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorize.API.REST.Start
{
    public static class SeedData
    {
        public static async Task EnsureSeedData(IHost host)
        {
            using (var serviceScope = host.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<AuthorizeDbContext>();
                await dbContext.Database.EnsureCreatedAsync();
                var authPermissions = serviceScope.ServiceProvider.GetRequiredService<IAuthPermissions>();
                AuthorizeDbContextSeed.SeedDefaultAsync(dbContext, authPermissions);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
