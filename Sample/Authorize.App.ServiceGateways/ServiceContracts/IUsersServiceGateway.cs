using Authorize.App.ServiceGateways.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authorize.App.ServiceGateways.ServiceContracts
{
    public interface IUsersServiceGateway
    {
        Task<IEnumerable<PermissionApplication>> GetPermissions(string username, string applicationName);
    }
}
