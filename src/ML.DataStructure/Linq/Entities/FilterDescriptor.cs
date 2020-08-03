using ML.DataStructure.Linq.Entities.Enums;

namespace ML.DataStructure.Linq.Entities
{
    public class FilterDescriptor
    {
        public string Member { get; set; }
        public object Value { get; set; }
        public FilterOperator Operator { get; set; }
    }
}
