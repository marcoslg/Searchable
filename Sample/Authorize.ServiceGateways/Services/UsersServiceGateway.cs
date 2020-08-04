using Authorize.ServiceGateways.Models.Common;
using Authorize.ServiceGateways.Models.Users;
using Authorize.ServiceGateways.ServiceContracts;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authorize.ServiceGateways.Services
{
    public class UsersServiceGateway : IUsersServiceGateway
    {
        private readonly ServiceContractsInter.IUsersServiceGateway _service;

        public UsersServiceGateway(ServiceContractsInter.IUsersServiceGateway service)
        {
            _service = service;
        }

        public Task Create(CreateUserRequest request)
            => _service.Create(request);

        public Task<IEnumerable<PermissionApplication>> GetPermissions(string username, string applicationName)
            => _service.GetPermissions(username, applicationName, $"{username}-{applicationName}");

        public Task AddRole([Body] UserRoleRequest request)
            => _service.AddRole(request);

        public Task RemoveRole([Body] UserRoleRequest request)
            => _service.RemoveRole(request);
    }
}
