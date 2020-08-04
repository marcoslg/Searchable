using System.Collections.Generic;

namespace Authorize.ServiceGateways.Models.Roles
{
    public class CreateRoleRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, IEnumerable<string>> Permissions { get; set; }
    }
}
