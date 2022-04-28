using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;

namespace BaseCorporate.Service.Model
{
    public class PageNotFoundLogListItem : BaseModel
    {
        public string PageUrl { get; set; }
        public string ReferrerUrl { get; set; }
        public string UserIp { get; set; }
        public string UserAgent { get; set; }
        public int Count { get; set; }
    }
}
