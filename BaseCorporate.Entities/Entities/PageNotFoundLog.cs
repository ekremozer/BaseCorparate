using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Entities.Infrastructure;

namespace BaseCorporate.Entities.Entities
{
    public class PageNotFoundLog : BaseEntity
    {
        public string PageUrl { get; set; }
        public string ReferrerUrl { get; set; }
        public string UserIp { get; set; }
        public string UserAgent { get; set; }
        public int Count { get; set; }
    }
}
