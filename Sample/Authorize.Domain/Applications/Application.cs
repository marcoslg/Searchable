using Authorize.Domain.Common;
using System.Collections.Generic;

namespace Authorize.Domain.Applications
{
    public class Application : AuditableEntity
    {
        public string _name;
        public string Name
        {
            get => _name;
            private set => _name = value?.ToLowerInvariant();
        }
        public string Description { get; set; }

        public Version Version { get; set; }
        public ICollection<Permission> Permissions { get; set; }

        public Application()
        { }
        public Application(string name)
        {
            Name = name;
            Permissions = new List<Permission>();
        }
    }
}