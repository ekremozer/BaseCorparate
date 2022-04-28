using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCorporate.Service.Model
{
    public class ArticleListViewItem
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string CategoryUrl { get; set; }
        public string Slug { get; set; }
        public string Image { get; set; }
        public string Summary { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
