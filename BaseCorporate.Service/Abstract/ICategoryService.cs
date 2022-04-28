using System;
using System.Collections.Generic;
using System.Text;
using BaseCorporate.Service.Infrastructure;
using BaseCorporate.Service.Model;
using BaseCorporate.Entities.Entities;

namespace BaseCorporate.Service.Abstract
{
    public interface ICategoryService
    {
        List<CategoryModel> GetList();
        CategoryModel Get(int id);
        CategoryModel AddOrUpdate(CategoryModel model);
        bool Delete(BaseModel model);
        CategoryModel AddModel();
        List<CategoryThreeModel> GetCategoriesThree(int categoryId);
    }
}
