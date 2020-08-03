using System.ComponentModel;

namespace ML.DataStructure.Linq.Entities
{
    public class SortDescriptor
    {
        public string Member { get; set; }
        public ListSortDirection SortDirection { get; set; }
    }
}
