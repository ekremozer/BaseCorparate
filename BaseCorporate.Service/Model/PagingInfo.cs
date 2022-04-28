using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCorporate.Service.Model
{
    public class PagingInfo
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItem { get; set; }
        public int TotalPage { get; set; }
    }
}
