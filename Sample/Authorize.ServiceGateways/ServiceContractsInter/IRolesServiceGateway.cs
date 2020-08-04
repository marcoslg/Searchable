using Authorize.ServiceGateways.Models.Roles;
using Refit;
using System.Threading.Tasks;
using Viseo.API.Servicegateway.Attributes;
using Viseo.API.Servicegateway.DelegationHandlers;

namespace Authorize.ServiceGateways.ServiceContractsInter
{
    public interface IRolesServiceGateway
    {
        [Headers("Authorization: Bearer")]
        [Get("/api/queries/roles/{roleName}")]
        Task<RolePermissions> Get(string roleName, [CacheParam("RoleCache", CacheOperations.Add)] string keyCode);

        [Headers("Authorization: Bearer")]
        [Post("/api/commands/roles")]
        Task Create([Body] CreateRoleRequest request);

        [Headers("Authorization: Bearer")]
        [Post("/api/commands/roles/AddPermission")]
        Task AddPermission([Body] AddPermissionRequest request);

        [Headers("Authorization: Bearer")]
        [Post("/api/commands/roles/RemovePermission")]
        Task RemovePermission([Body] RemovePermissionRequest request);

    }
}
