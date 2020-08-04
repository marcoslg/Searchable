using Authorize.App.ServiceGateways.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Viseo.API.Servicegateway.Attributes;
using Viseo.API.Servicegateway.DelegationHandlers;

namespace Authorize.App.ServiceGateways.ServiceContractsInter
{
    public interface IUsersServiceGateway
    {
        [Headers("Authorization: Bearer")]
        [Get("/api/queries/users/{username}/{applicationName}/permissions")]
        Task<IEnumerable<PermissionApplication>> GetPermissions(string username, string applicationName, [CacheParam("UserApplicationCache", CacheOperations.Add)] string keyCode);
    }
}
