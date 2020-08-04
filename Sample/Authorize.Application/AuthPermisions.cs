using Authorize.Domain.Applications;
using System.Collections.Generic;

namespace Authorize.Application
{
    public interface IAuthPermissions
    {
        IEnumerable<Permission> Permissions { get; }
    }
    public class AuthPermissions : IAuthPermissions
    {
        #region roles
        public const string RoleCreated = "role.created";
        public const string RoleDisabled = "role.disabled";
        public const string RoleEnabled = "role.enabled";
        public const string RoleGet = "role.get";
        public const string RoleSearch = "role.searchrole";
        #endregion roles

        #region users
        public const string UserCreated = "user.created";
        public const string UserDisabled = "user.disabled";
        public const string UserEnabled = "user.enabled";
        public const string UserGet = "user.get";
        public const string UserSearch = "user.searchrole";
        #endregion users

        #region applications
        public const string ApplicationRemoved = "application.removed";
        public const string ApplicationCreated = "application.created";
        public const string ApplicationDisabled = "application.disabled";
        public const string ApplicationEnabled = "application.enabled";
        public const string ApplicationGet = "application.get";
        public const string ApplicationSearch = "application.searchrole";
        #endregion applications


        public IEnumerable<Permission> Permissions { get; private set; }

        public AuthPermissions()
        {
            Permissions = new List<Permission>()
            {
                #region roles
                Permission.For(RoleCreated),
                Permission.For(RoleDisabled),
                Permission.For(RoleEnabled),
                Permission.For(RoleGet),
                Permission.For(RoleSearch),
                #endregion roles

                #region users
                Permission.For(UserCreated),
                Permission.For(UserDisabled),
                Permission.For(UserEnabled),
                Permission.For(UserGet),
                Permission.For(UserSearch),
                #endregion users

                #region applications
                Permission.For(ApplicationRemoved),
                Permission.For(ApplicationCreated),
                Permission.For(ApplicationDisabled),
                Permission.For(ApplicationEnabled),
                Permission.For(ApplicationGet),
                Permission.For(ApplicationSearch),
                #endregion applications
            };
        }
    }
}