using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCorporate.Service.Model
{
    public class MenuGroupUIModel
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public List<MenuItemUIModel> Items { get; set; }

        public MenuGroupUIModel()
        {
            Items = new List<MenuItemUIModel>();
        }
    }

    public class MenuItemUIModel
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public bool External { get; set; }
        public List<MenuSubItemUIModel> SubItems { get; set; }

        public MenuItemUIModel()
        {
            SubItems = new List<MenuSubItemUIModel>();
        }
    }

    public class MenuSubItemUIModel
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public bool External { get; set; }
    }
}
