using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;

namespace BaseCorporate.Service.Model
{
    public class CategoryModel : BaseModel
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public int? ParentCategoryId { get; set; }
        public List<ListItem> ParentCategories { get; set; }
        public string Slug { get; set; }
        public string BreadCrumb { get; set; }
    }
}
