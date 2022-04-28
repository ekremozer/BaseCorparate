using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Entities.Infrastructure;

namespace BaseCorporate.Entities.Entities
{
    public class RedirectRecord : BaseEntity
    {
        public string OldUrl { get; set; }
        public string NewUrl { get; set; }
        public int RedirectCount { get; set; }
    }
}
