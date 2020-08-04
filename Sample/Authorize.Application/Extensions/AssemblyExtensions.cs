using System;
using System.Linq;
using System.Reflection;

namespace Authorize.Application.Extensions
{
    public static class AssemblyExtensions
    {
        public static string GetApplicationName(this Assembly assembly) => GetAttributeValue<AssemblyProductAttribute>(assembly);

        public static string GetVersion(this Assembly assembly)
        {
            var infoVersion = GetAttributeValue<AssemblyInformationalVersionAttribute>(assembly);
            if (infoVersion != null)
            {
                return infoVersion;
            }

            return GetAttributeValue<AssemblyFileVersionAttribute>(assembly);
        }


        public static string GetAttributeValue<T>(this Assembly assembly)
            where T : Attribute
        {
            var type = typeof(T);
            var attribute = assembly
                .CustomAttributes
                .Where(x => x.AttributeType == type)
                .Select(x => x.ConstructorArguments.FirstOrDefault())
                .FirstOrDefault();
            return attribute == null ? string.Empty : attribute.Value.ToString();
        }
    }
}
