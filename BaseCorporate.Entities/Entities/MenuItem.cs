using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Entities.Infrastructure;

namespace BaseCorporate.Entities.Entities
{
    public class MenuItem : BaseEntity
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public bool External { get; set; }
        public int GroupId { get; set; }
        public int OrderBy { get; set; }
        public MenuGroup Group { get; set; }
        public ICollection<MenuSubItem> SubItems { get; set; }

        public MenuItem()
        {
            SubItems = new HashSet<MenuSubItem>();
            OrderBy = 0;
        }
    }
}
