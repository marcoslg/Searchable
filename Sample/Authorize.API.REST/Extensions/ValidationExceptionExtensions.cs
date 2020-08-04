using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Authorize.API.REST.Extensions
{
    internal static class ValidationExceptionExtensions
    {
        public static IDictionary<string,string[]> GetErrors(this ValidationException validException)
        => validException.Errors.ToDictionary(x => x.ErrorCode, x => new string[] { x.ErrorMessage });
    }
}
