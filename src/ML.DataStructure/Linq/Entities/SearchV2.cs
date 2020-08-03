using System.Collections.Generic;

namespace ML.DataStructure.Linq.Entities
{
    public class SearchV2
    {
        public List<UnionFilterDescriptor> UnionFilterDescriptors = new List<UnionFilterDescriptor>();
        public List<SortDescriptor> SortDescriptors = new List<SortDescriptor>();
        public int? PageSize { get; set; }
        public int? Page { get; set; }
    }
}
