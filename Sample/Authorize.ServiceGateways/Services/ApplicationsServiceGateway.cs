using Authorize.ServiceGateways.Models.Applications;
using Authorize.ServiceGateways.Models.Common;
using Authorize.ServiceGateways.ServiceContracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authorize.ServiceGateways.Services
{
    public class ApplicationsServiceGateway : IApplicationsServiceGateway
    {
        private readonly ServiceContractsInter.IApplicationsServiceGateway _service;

        public ApplicationsServiceGateway(ServiceContractsInter.IApplicationsServiceGateway service)
        {
            _service = service;
        }

        public Task<ApplicationPermissions> Get(string applicationName)
            => _service.Get(applicationName, applicationName);

        public Task<IEnumerable<PermissionApplication>> GetPermissions(string applicationName)
            => _service.GetPermissions(applicationName, applicationName);

        public Task<IEnumerable<Application>> SearchApplication(string applicationName, int? page, int? pageSize)
            => _service.SearchApplication(applicationName, applicationName, page, pageSize);

        public Task Registry(CreateApplicationRequest request)
            => _service.Registry(request);

        public Task RemoveApplication(RemoveApplicationRequest request)
                => _service.RemoveApplication(request);

    }
}
