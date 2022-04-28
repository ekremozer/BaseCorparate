using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Entities.Infrastructure;

namespace BaseCorporate.Entities.Entities
{
    public class UrlRecord : BaseEntity
    {
        public string Slug { get; set; }
        public int? ArticleId { get; set; }
        public Article Article { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int? TagId { get; set; }
        public Tag Tag { get; set; }
        public int? TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
