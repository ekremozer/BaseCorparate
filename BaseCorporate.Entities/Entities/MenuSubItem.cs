using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Entities.Infrastructure;

namespace BaseCorporate.Entities.Entities
{
    public class MenuSubItem : BaseEntity
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public bool External { get; set; }
        public int ItemId { get; set; }
        public int OrderBy { get; set; }
        public MenuItem Item { get; set; }

        public MenuSubItem()
        {
            OrderBy = 0;
        }
    }
}
