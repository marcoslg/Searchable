using Authorize.Domain.Roles;
using Authorize.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Authorize.Application.Contracts
{
    public interface IAppDbContext 
    {
        DbSet<User> Users { get; }
        DbSet<Role> Roles { get; }
        DbSet<Domain.Applications.Application> Applications { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
