using System;
using System.Collections.Generic;
using System.Text;

namespace Authorize.Application.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException()
            : base()
        {
        }     

        public ForbiddenException(string name, Exception innerException)
            : base($"operation \"{name}\" have not access.", innerException)
        {
        }

        public ForbiddenException(string name)
            : base($"operation \"{name}\" have not access.")
        {
        }
    }
}
