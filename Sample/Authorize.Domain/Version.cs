using Authorize.Domain.Common;
using System.Collections.Generic;

namespace Authorize.Domain
{
    public sealed class Version : ValueObject
    {
        public uint Major { get; internal set; }
        public uint Minor { get; internal set; }
        public uint Minus { get; internal set; }

        private Version(uint major, uint minor, uint minus)
        {
            Major = major;
            Minor = minor;
            Minus = minus;
        }
        public static Version For(string version)
        {
            var splVersion = version.Split('.');
            return new Version(uint.Parse(splVersion[0]), uint.Parse(splVersion[1]), uint.Parse(splVersion[2]));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Major;
            yield return Minor;
            yield return Minus;
        }
        public override string ToString()
        {
            return $"{Major}.{Minor}.{Minus}";
        }
    }
}
