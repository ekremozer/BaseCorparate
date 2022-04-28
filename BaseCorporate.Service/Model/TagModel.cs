using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;

namespace BaseCorporate.Service.Model
{
    public class TagModel : BaseModel
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public int ArticleCount { get; set; }
    }
}
