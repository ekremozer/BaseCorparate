using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;

namespace BaseCorporate.Service.Model
{
    public class MenuGroupModel : BaseModel
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public int ItemCount { get; set; }
    }
}
