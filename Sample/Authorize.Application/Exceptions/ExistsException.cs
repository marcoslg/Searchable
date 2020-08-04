using System;

namespace Authorize.Application.Exceptions
{
    public class ExistsException : Exception
    {
        public ExistsException()
            : base()
        {
        }

        public ExistsException(string message)
            : base(message)
        {
        }

        public ExistsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ExistsException(string name, object key)
            : base($"Entity \"{name}\" ({key}) already exists.")
        {
        }
    }
}
