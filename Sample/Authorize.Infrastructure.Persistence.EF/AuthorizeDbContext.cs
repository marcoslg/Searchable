using Authorize.Application.Contracts;
using Authorize.Domain.Roles;
using Authorize.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Authorize.Infrastructure.Persistence.EF
{
    public class AuthorizeDbContext : DbContext, IAppDbContext
    {
       
        #region contructors
        public AuthorizeDbContext(
            DbContextOptions options)
            : base(options)
        {
            if (Database.IsSqlServer())
            {
                Database.AutoTransactionsEnabled = true;
            }
           
        }
        #endregion contructors
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Domain.Applications.Application> Applications { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
