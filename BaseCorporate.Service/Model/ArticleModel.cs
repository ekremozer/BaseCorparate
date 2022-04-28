using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;

namespace BaseCorporate.Service.Model
{
    public class ArticleModel : BaseModel
    {
        public string PageTitle { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Body { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string Tags { get; set; }
        public int? CategoryId { get; set; }
        public List<ListItem> Categories { get; set; }
        public bool? AllowComment { get; set; }
        public string Slug { get; set; }
        public string SiteMapPriority { get; set; }

        public ArticleModel()
        {
            Categories = new List<ListItem>();
        }
    }
}
