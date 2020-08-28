using ML.DataStructure.Linq.Entities;
using ML.DataStructure.Parsers;

namespace ML.DataStructure.Collections.DTO
{
    public class SearchDto
    {

        public string Filter
        {
            get;
            set;
        }

        public string OrderBy
        {
            get;
            set;
        }

        public int? PageSize
        {
            get;
            set;
        }

        public int? Page
        {
            get;
            set;
        }

        public Search ToMap()
        {
            var result = new Search()
            {
                Page = Page,
                PageSize = PageSize
            };
            result.FilterDescriptors = FilterDescriptorParser.ListFromString(Filter);
            result.SortDescriptors = SortDescriptorParser.ListFromString(OrderBy);
            return result;
        }
    }
}
