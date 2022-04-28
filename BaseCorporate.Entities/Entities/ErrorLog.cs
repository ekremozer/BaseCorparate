using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Entities.Infrastructure;

namespace BaseCorporate.Entities.Entities
{
    public class ErrorLog : BaseEntity
    {
        public string ExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
        public string UserIp { get; set; }
        public string PageUrl { get; set; }
        public string ReferrerUrl { get; set; }
    }
}
