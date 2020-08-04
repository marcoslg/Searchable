using Authorize.ServiceGateways.Models.Applications;
using Authorize.ServiceGateways.Models.Common;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Viseo.API.Servicegateway.Attributes;
using Viseo.API.Servicegateway.DelegationHandlers;

namespace Authorize.ServiceGateways.ServiceContracts
{
    public interface IApplicationsServiceGateway
    {
        Task<ApplicationPermissions> Get(string applicationName);

        Task<IEnumerable<PermissionApplication>> GetPermissions(string applicationName);

        Task<IEnumerable<Application>> SearchApplication(string applicationName, int? page, int? pageSize);

        Task Registry([Body] CreateApplicationRequest request);

        Task RemoveApplication([Body] RemoveApplicationRequest request);
    }
}
