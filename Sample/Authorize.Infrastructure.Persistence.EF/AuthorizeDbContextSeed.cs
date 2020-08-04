using Authorize.Application;
using Authorize.Application.Contracts;
using Authorize.Domain.Applications;
using Authorize.Domain.Roles;
using Authorize.Domain.Users;
using System.Collections.Generic;
using System.Linq;

namespace Authorize.Infrastructure.Persistence.EF
{
    public static class AuthorizeDbContextSeed
    {

        public static void SeedDefaultAsync(IAppDbContext context, IAuthPermissions authPermissions)
        {
            SeedApplication(context, authPermissions);           
        }


        private static void SeedApplication(IAppDbContext context, IAuthPermissions authPermissions)
        {
            string[] apiAuthNames = { "authorize.application", "authorize.application1", "authorize.application2" };
            foreach (var apiAuthName in apiAuthNames)
            {
                var app = context.Applications.Where(a => a.Name == apiAuthName).FirstOrDefault();
                if (app == null)
                {
                    app = new Authorize.Domain.Applications.Application(apiAuthName)
                    {
                        Permissions = authPermissions.Permissions.ToList()
                    };
                    context.Applications.Add(app);
                }
            }            
        }

    }
}
