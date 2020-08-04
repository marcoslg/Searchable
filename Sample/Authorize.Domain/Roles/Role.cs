using Authorize.Domain.Common;
using Authorize.Domain.Relations;
using Authorize.Domain.Users;
using System.Collections.Generic;

namespace Authorize.Domain.Roles
{
    public class Role : AuditableEntity
    {
        private string _name;
        public string Name
        {
            get => _name;
            private set => _name = value?.ToLowerInvariant();
        }
        public string Description { get; set; }
        public ICollection<ApplicationRole> Applications { get; set; }

        public Role()
        { }
        public Role(string roleName)
            : this(roleName, string.Empty)
        {
        }

        public Role(string roleName, string description, ICollection<ApplicationRole> applications = null)
        {
            Name = roleName;
            Description = description;
            Applications = applications ?? new List<ApplicationRole>();
            IsEnabled = true;
            Users = new List<UserRole>();
        }

        public ICollection<UserRole> Users { get; set; }

    }  
}
