using System.Collections.Generic;

namespace ML.DataStructure.Linq.Entities
{
    public class Search
    {
        public List<FilterDescriptor> FilterDescriptors = new List<FilterDescriptor>();
        public List<SortDescriptor> SortDescriptors = new List<SortDescriptor>();
        public int? PageSize { get; set; }
        public int? Page { get; set; }
    }
}
