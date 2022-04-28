using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCorporate.Service.Model
{
    public class ArticleAdminListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
