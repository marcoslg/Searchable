using ML.DataStructure.Linq.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ML.DataStructure.Parsers
{
    public static class SortDescriptorParser
    {
        public static List<SortDescriptor> ListFromString(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return new List<SortDescriptor>();
            }
            var filterDescriptors = filter.Split('|');
            return filterDescriptors.Select(filterString => FromString(filterString)).ToList();
        }
        public static SortDescriptor FromString(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return null;
            }
            var filterSplit = filter.Split(' ');
            var member = filterSplit[0];
            var op = filterSplit[1];
            return new SortDescriptor()
            {
                Member = member,
                SortDirection = OperatorFromString(op),

            };
        }

        internal static ListSortDirection OperatorFromString(string op)
        {
            switch (op.ToLowerInvariant())
            {
                case "asc":
                    return ListSortDirection.Ascending;
                case "desc":
                    return ListSortDirection.Descending;
                default:
                    return ListSortDirection.Descending;
            }

        }
    }
}
