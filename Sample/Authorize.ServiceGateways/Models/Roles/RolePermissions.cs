using System.Collections.Generic;

namespace Authorize.ServiceGateways.Models.Roles
{
    public class RolePermissions : Role
    {
        public IDictionary<string, IEnumerable<string>> Permissions { get; set; }
    }
}
