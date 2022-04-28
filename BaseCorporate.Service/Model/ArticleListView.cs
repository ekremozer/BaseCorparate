using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCorporate.Service.Model
{
    public class ArticleListView
    {
        public string BreadCrumb { get; set; }
        public string PageTitle { get; set; }
        public string Slug { get; set; }
        public List<ArticleListViewItem> ArticleList { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public HeadModel HeadModel { get; set; }
        public int CategoryId { get; set; }
        public bool CategoryLink { get; set; } = true;
    }
}
