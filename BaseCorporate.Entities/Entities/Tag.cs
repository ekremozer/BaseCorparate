using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Entities.Infrastructure;

namespace BaseCorporate.Entities.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public UrlRecord UrlRecord { get; set; }
        public int? UrlRecordId { get; set; }
        public ICollection<TagMapping> Mappings { get; set; }

        public Tag()
        {
            Mappings = new HashSet<TagMapping>();
        }
    }
}
