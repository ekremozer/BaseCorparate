using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;

namespace BaseCorporate.Service.Model
{
    public class MenuSubItemModel : BaseModel
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string GroupKey { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public bool External { get; set; }
        public int OrderBy { get; set; }
        public List<ListItem> Items { get; set; }
    }
}
