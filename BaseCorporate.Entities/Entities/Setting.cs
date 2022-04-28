using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Entities.Infrastructure;

namespace BaseCorporate.Entities.Entities
{
    public class Setting : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string GroupName { get; set; }
    }
}
