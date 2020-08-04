using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authorize.API.REST.Start;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Authorize.API.REST
{
    public class Program
    {
        private const string SeedArgs = "/seed";
        public static async Task Main(string[] args)
        {
            var seed = args.Any(x => x == SeedArgs);
            if (seed) args = args.Except(new[] { SeedArgs }).ToArray();
            var host = CreateHostBuilder(args).Build();
            // Uncomment this to seed upon startup, alternatively pass in `dotnet run /seed` to seed using CLI
            // await DbMigrationHelpers.EnsureSeedData<IdentityServerConfigurationDbContext, AdminIdentityDbContext, IdentityServerPersistedGrantDbContext, AdminLogDbContext, AdminAuditLogDbContext, UserIdentity, UserIdentityRole>(host);
            if (seed)
            {
                await SeedData.EnsureSeedData(host);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
