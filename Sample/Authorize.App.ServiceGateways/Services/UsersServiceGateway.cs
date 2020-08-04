using Authorize.App.ServiceGateways.Models;
using Authorize.App.ServiceGateways.ServiceContracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authorize.App.ServiceGateways.Services
{
    public class UsersServiceGateway : IUsersServiceGateway
    {
        private readonly ServiceContractsInter.IUsersServiceGateway _service;

        public UsersServiceGateway(ServiceContractsInter.IUsersServiceGateway service)
        {
            _service = service;
        }

        public Task<IEnumerable<PermissionApplication>> GetPermissions(string username, string applicationName)
            => _service.GetPermissions(username, applicationName, $"{username}-{applicationName}");
    }
}
