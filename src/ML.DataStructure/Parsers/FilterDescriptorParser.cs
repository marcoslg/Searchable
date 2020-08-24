using ML.DataStructure.Linq.Entities;
using ML.DataStructure.Linq.Entities.Enums;
using System.Collections.Generic;
using System.Linq;

namespace ML.DataStructure.Parsers
{
    public static class FilterDescriptorParser
    {

        public static List<FilterDescriptor> ListFromString(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return new List<FilterDescriptor>();
            }
            var filterDescriptors = filter.Split('|');
            return filterDescriptors.Select(filterString => FromString(filterString)).ToList();
        }
        public static FilterDescriptor FromString(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return null;
            }
            var filterSplit = filter.Split(' ');
            var member = filterSplit[0];
            var op = filterSplit[1];
            var filterDescriptor = new FilterDescriptor()
            {
                Member = member,
                Operator = OperatorFromString(op),
            };

            var value = filterSplit[2];
            bool valueBool;
            if (bool.TryParse(value, out valueBool))
            {
                filterDescriptor.Value = valueBool;
            }
            else
            {
                filterDescriptor.Value = value;
            }
            return filterDescriptor;
        }

        internal static FilterOperator OperatorFromString(string op)
        {
            switch (op)
            {
                case "lt":
                    return FilterOperator.IsLessThan;
                case "lte":
                    return FilterOperator.IsLessThanOrEqualTo;
                case "eq":
                    return FilterOperator.IsEqualTo;
                case "neq":
                    return FilterOperator.IsNotEqualTo;
                case "gte":
                    return FilterOperator.IsGreaterThanOrEqualTo;
                case "gt":
                    return FilterOperator.IsGreaterThan;
                case "str":
                    return FilterOperator.StartsWith;
                case "end":
                    return FilterOperator.EndsWith;
                case "cnt":
                    return FilterOperator.Contains;
                case "ncn":
                    return FilterOperator.DoesNotContain;
                default:
                    return FilterOperator.Contains;
            }

        }

    }
}
