using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Entities.Infrastructure;

namespace BaseCorporate.Entities.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public string Slug { get; set; }
        public string DisplayName { get; set; }
        public string BreadCrumb { get; set; }
        public UrlRecord UrlRecord { get; set; }
        public int? UrlRecordId { get; set; }
        public ICollection<Category> ChildCategories { get; set; }
        public ICollection<Article> Articles { get; set; }

        public Category()
        {
            ChildCategories = new HashSet<Category>();
            Articles = new HashSet<Article>();
        }
    }
}
