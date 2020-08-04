using Authorize.ServiceGateways.Models.Common;
using Authorize.ServiceGateways.Models.Users;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Viseo.API.Servicegateway.Attributes;
using Viseo.API.Servicegateway.DelegationHandlers;

namespace Authorize.ServiceGateways.ServiceContractsInter
{
    public interface IUsersServiceGateway
    {
        [Headers("Authorization: Bearer")]
        [Get("/api/queries/users/{username}/{applicationName}/permissions")]
        Task<IEnumerable<PermissionApplication>> GetPermissions(string username, string applicationName, [CacheParam("UserApplicationCache", CacheOperations.Add)] string keyCode);

        [Headers("Authorization: Bearer")]
        [Post("/api/commands/Users")]
        Task Create([Body] CreateUserRequest request);

        [Headers("Authorization: Bearer")]
        [Post("/api/commands/Users/AddRole")]
        Task AddRole([Body] UserRoleRequest request);

        [Headers("Authorization: Bearer")]
        [Post("/api/commands/Users/RemoveRole")]
        Task RemoveRole([Body] UserRoleRequest request);
    }
}
