using Authorize.Domain.Roles;
using Authorize.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authorize.Domain.Relations
{
    public class UserRole
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
