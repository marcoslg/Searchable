using Authorize.ServiceGateways.Models.Applications;
using Authorize.ServiceGateways.Models.Common;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Viseo.API.Servicegateway.Attributes;
using Viseo.API.Servicegateway.DelegationHandlers;

namespace Authorize.ServiceGateways.ServiceContractsInter
{
    public interface IApplicationsServiceGateway
    {
        [Headers("Authorization: Bearer")]
        [Get("/api/queries/applications/{applicationName}")]
        Task<ApplicationPermissions> Get(string applicationName, [CacheParam("ApplicationCache", CacheOperations.Add)] string keyCode);

        [Headers("Authorization: Bearer")]
        [Get("/api/queries/applications/{applicationName}/permissions")]
        Task<IEnumerable<PermissionApplication>> GetPermissions(string applicationName, [CacheParam("ApplicationPermissionsCache", CacheOperations.Add)] string keyCode);

        [Headers("Authorization: Bearer")]
        [Get("/api/queries/applications/Search/{applicationName}")]
        Task<IEnumerable<Application>> SearchApplication(string applicationName, [CacheParam("ApplicationSearchCache", CacheOperations.Add)] string keyCode, int? page, int? pageSize);

        [Headers("Authorization: Bearer")]
        [Post("/api/commands/Applications")]
        Task Registry([Body] CreateApplicationRequest request);

        [Headers("Authorization: Bearer")]
        [Post("/api/commands/Applications/RemoveApplication")]
        Task RemoveApplication([Body] RemoveApplicationRequest request);
    }
}
