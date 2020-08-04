using Authorize.ServiceGateways.Models.Common;
using Authorize.ServiceGateways.Models.Users;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Viseo.API.Servicegateway.Attributes;
using Viseo.API.Servicegateway.DelegationHandlers;

namespace Authorize.ServiceGateways.ServiceContracts
{
    public interface IUsersServiceGateway
    {
        Task<IEnumerable<PermissionApplication>> GetPermissions(string username, string applicationName);

        Task Create([Body] CreateUserRequest request);

        Task AddRole([Body] UserRoleRequest request);

        Task RemoveRole([Body] UserRoleRequest request);
    }
}
