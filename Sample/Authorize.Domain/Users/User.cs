using Authorize.Domain.Common;
using Authorize.Domain.Relations;
using Authorize.Domain.Roles;
using System.Collections.Generic;

namespace Authorize.Domain.Users
{
    public class User : AuditableEntity
    {
        private string _userName;
        public string UserName 
        {
            get => _userName;
            private set => _userName = value?.ToLowerInvariant();
        }
        public User()
        {

        }
        public User(string username)
          : this (username, null)
        {

        }
        public User(string username, ICollection<Role> roles)
        {
            UserName = username;

            Roles = Build(roles) ?? new List<UserRole>();
        }

        public ICollection<UserRole> Roles { get; set; }

        private ICollection<UserRole> Build(ICollection<Role> roles)
        {
            ICollection<UserRole> result = new List<UserRole>();
            if (roles != null)
            {
                foreach(var role in roles)
                {
                    result.Add(new UserRole()
                    {
                        User = this,
                        Role = role
                    });
                }
            }
            return result;
        }
    }
}
