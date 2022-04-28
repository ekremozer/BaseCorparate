using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCorporate.Service.Model
{
    public class PagedList<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int ItemsPerPage { get; set; } = 20;
        public int GetSkipCount(int page)
        {
            var skipCount = page <= 1 ? 0 : (page - 1) * ItemsPerPage;

            return skipCount;
        }
    }
}
