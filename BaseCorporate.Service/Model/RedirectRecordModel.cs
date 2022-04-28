using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;

namespace BaseCorporate.Service.Model
{
    public class RedirectRecordModel : BaseModel
    {
        public string OldUrl { get; set; }
        public string NewUrl { get; set; }
        public int RedirectCount { get; set; }
    }
}
