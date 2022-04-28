using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCorporate.Service.Model
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string PageTitle { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Body { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string Tags { get; set; }
        public bool? AllowComment { get; set; }
        public string Slug { get; set; }
        public string BreadCrumb { get; set; }
        public DateTime CreatedAt { get; set; }
        public HeadModel HeadModel { get; set; }
    }
}
