using ML.DataStructure.Linq.Entities;
using ML.DataStructure.Parsers;

namespace ML.DataStructure.Collections.DTO
{
    public class SearchDto
    {

        public string FilterDescriptors
        {
            get;
            set;
        }

        public string SortDescriptors
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
            result.FilterDescriptors = FilterDescriptorParser.ListFromString(org.FilterDescriptors);
            result.SortDescriptors = SortDescriptorParser.ListFromString(org.SortDescriptors);
            return result;
        }
    }
}
