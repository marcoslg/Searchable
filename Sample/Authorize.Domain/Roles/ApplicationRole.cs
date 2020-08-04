using Authorize.Domain.Applications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authorize.Domain.Roles
{
    public class ApplicationRole
    {
        public Application Application { get; set; }
        public ICollection<Permission> Permissions { get; set; }
    }
}
