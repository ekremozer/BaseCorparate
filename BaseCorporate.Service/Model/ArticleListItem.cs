using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;

namespace BaseCorporate.Service.Model
{
    public class ArticleListItem : BaseModel
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Slug { get; set; }
        public string Image { get; set; }
        public string Summary { get; set; }
        public string MetaDescription { get; set; }
    }
}
