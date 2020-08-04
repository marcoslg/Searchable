using Authorize.Domain.Common;
using System.Collections.Generic;

namespace Authorize.Domain.Applications
{
    public class Permission : ValueObject
    {
        public string _name;
        public string Name
        {
            get => _name;
            private set => _name = value?.ToLowerInvariant();
        }
        public string Description { get; private set; }

        private Permission(string name) {
            Name = name;
        }
        private Permission(string name, string description) 
        {
            Name = name;
            Description = description;
        }
        public static Permission For(string name)
        => new Permission(name);
        public static Permission For(string name, string description)
        => new Permission(name, description);

        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? base.GetHashCode();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Description;
        }
    }
}
