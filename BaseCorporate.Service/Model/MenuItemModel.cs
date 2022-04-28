using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;

namespace BaseCorporate.Service.Model
{
    public class MenuItemModel : BaseModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupKey { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public bool External { get; set; }
        public int OrderBy { get; set; }
        public List<ListItem> Groups { get; set; }
    }
}
