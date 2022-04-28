using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;

namespace BaseCorporate.Service.Model
{
    public class SettingModel : BaseModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string GroupName { get; set; }
    }
}
