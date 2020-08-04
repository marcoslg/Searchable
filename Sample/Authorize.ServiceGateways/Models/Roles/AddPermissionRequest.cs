using System.Collections.Generic;

namespace Authorize.ServiceGateways.Models.Roles
{
    public class AddPermissionRequest
    {
        public string Name { get; set; }
        public Dictionary<string, IEnumerable<string>> Permissions { get; set; }
    }
}
