using System;
using System.Collections.Generic;
using System.Text;

namespace Authorize.Application.Extensions
{
    internal static class ICollectionExtensions
    {

        public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> add)
            where T : class
        {
            foreach (var item in add)
            {
                source.Add(item);
            }
        }
    }
}
