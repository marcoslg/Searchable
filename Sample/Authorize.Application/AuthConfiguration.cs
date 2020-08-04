using Authorize.Application.Extensions;
using System.Reflection;

namespace Authorize.Application
{

    public interface IAuthConfiguration
    {
        string ApplicationName { get; }
        Domain.Version Version { get; }
    }
    public class AuthConfiguration : IAuthConfiguration
    {

        public string ApplicationName { get; private set; }
        public Domain.Version Version { get; private set; }
        public AuthConfiguration()
        {
            ApplicationName = Assembly.GetExecutingAssembly().GetApplicationName();
            Version = Domain.Version.For(Assembly.GetExecutingAssembly().GetVersion());
        }
    }
}
