using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Entities.Infrastructure;

namespace BaseCorporate.Entities.Entities
{
    public class MenuGroup : BaseEntity
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public ICollection<MenuItem> Items { get; set; }

        public MenuGroup()
        {
            Items = new HashSet<MenuItem>();
        }
    }
}
