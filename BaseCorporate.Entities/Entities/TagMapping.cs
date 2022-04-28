using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Entities.Infrastructure;

namespace BaseCorporate.Entities.Entities
{
    public class TagMapping : BaseEntity
    {
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
