using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Entities.Infrastructure;

namespace BaseCorporate.Entities.Entities
{
    public class Article : BaseEntity
    {
        public string PageTitle { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Body { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public bool? AllowComment { get; set; }
        public string Slug { get; set; }
        public string SiteMapPriority { get; set; }
        public UrlRecord UrlRecord { get; set; }
        public int? UrlRecordId { get; set; }

        public ICollection<TagMapping> TagMappings { get; set; }

        public Article()
        {
            TagMappings = new HashSet<TagMapping>();
        }
    }
}
