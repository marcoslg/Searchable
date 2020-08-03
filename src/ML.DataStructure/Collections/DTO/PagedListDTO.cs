using System;
using System.Collections.Generic;

namespace ML.DataStructure.Collections.DTO
{
    [Serializable]
    public class PagedListDTO<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public List<T> Rows { get; set; }

    }
}
