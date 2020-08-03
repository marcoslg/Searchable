using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML.DataStructure.Linq.Entities.Enums;

namespace ML.DataStructure.Linq.Entities
{
    public class UnionFilterDescriptor
    {
        public List<FilterDescriptor> FilterDescriptors = new List<FilterDescriptor>();
        public UnionOperator UnionOperator { get; set; }
    }
}
