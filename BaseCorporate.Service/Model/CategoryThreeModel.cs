using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCorporate.Service.Model
{
    public class CategoryThreeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int? ParentCategoryId { get; set; }
        public List<CategoryThreeModel> SubCategories { get; set; }
    }
}
