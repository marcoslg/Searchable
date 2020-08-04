using System;
using System.Collections.Generic;
using System.Text;

namespace Authorize.ServiceGateways.Models.Users
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }
        public List<string> RoleNames { get; set; }
    }
}
