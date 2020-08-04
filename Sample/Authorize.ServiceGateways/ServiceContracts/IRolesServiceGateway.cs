using Authorize.ServiceGateways.Models.Roles;
using System.Threading.Tasks;

namespace Authorize.ServiceGateways.ServiceContracts
{
    public interface IRolesServiceGateway
    {
        Task<RolePermissions> Get(string roleName);

        Task Create(CreateRoleRequest request);

        Task AddPermission(AddPermissionRequest request);

        Task RemovePermission(RemovePermissionRequest request);
    }
}
