using System.Collections.Generic;

namespace Authorize.ServiceGateways.Models.Applications
{
    public class ApplicationPermissions : Application
    {
        public IDictionary<string, IEnumerable<string>> Permissions { get; set; }
    }
}
