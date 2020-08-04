using System;

namespace Authorize.Application.Exceptions
{
    public class DisabledException : Exception
    {
        public DisabledException()
            : base()
        {
        }

        public DisabledException(string message)
            : base(message)
        {
        }

        public DisabledException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public DisabledException(string name, object key)
            : base($"Entity \"{name}\" ({key}) had locked.")
        {
        }
    }
}
