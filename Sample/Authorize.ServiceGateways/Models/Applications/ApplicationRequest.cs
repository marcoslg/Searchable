using Authorize.ServiceGateways.Models.Common;
using System.Collections.Generic;

namespace Authorize.ServiceGateways.Models.Applications
{
    public class ApplicationRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<PermissionApplication> Permissions { get; set; }
    }
}
