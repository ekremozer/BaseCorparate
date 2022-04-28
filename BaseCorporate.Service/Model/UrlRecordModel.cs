using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;

namespace BaseCorporate.Service.Model
{
    public class UrlRecordModel : BaseModel
    {
        public string Slug { get; set; }
        public int? ArticleId { get; set; }
        public int? CategoryId { get; set; }
        public int? TagId { get; set; }
        public int? TopicId { get; set; }
        public bool UpdateEntity { get; set; }

        public UrlRecordModel()
        {
            UpdateEntity = false;
        }
    }
}
