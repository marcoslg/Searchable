using Authorize.ServiceGateways.Models.Roles;
using Authorize.ServiceGateways.ServiceContracts;
using System.Threading.Tasks;

namespace Authorize.ServiceGateways.Services
{
    public class RolesServiceGateway : IRolesServiceGateway
    {
        private readonly ServiceContractsInter.IRolesServiceGateway _service;

        public RolesServiceGateway(ServiceContractsInter.IRolesServiceGateway service)
        {
            _service = service;
        }

        public Task Create(CreateRoleRequest request)
            => _service.Create(request);

        public Task<RolePermissions> Get(string roleName)
            => _service.Get(roleName, roleName);

        public Task AddPermission(AddPermissionRequest request)
            => _service.AddPermission(request);

        public Task RemovePermission(RemovePermissionRequest request)
            => _service.RemovePermission(request);

    }
}
